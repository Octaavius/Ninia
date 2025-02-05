using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public List<GameObject> ProjectilePrefabs;
    [HideInInspector] public List<GameObject> BuffPrefabs;
    [HideInInspector] public List<GameObject> MobPrefabs;
    [HideInInspector] public float BuffChance;
    
    //public Transform spawnPoint;
    private Coroutine spawnCoroutine = null;
    public float minSpawnTime = 2.0f;
    public float maxSpawnTime = 3.0f;
    [SerializeField] private float maxDelta = 1f;
    [SerializeField] private Direction spawnDirection = Direction.Down;
    private bool isSpawning = false;
    private float _originalMinSpawnTime;
    private float _originalMaxSpawnTime;
    private float _originalProjectileSpeed;

    void Start()
    {
        SpawnerManager.Instance.RegisterSpawner(this);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        if (spawnCoroutine == null)
        {
            switch (SceneManagerScript.Instance.sceneName)
            {
                case "Monsters":
                    spawnCoroutine = StartCoroutine(SpawnMobs());
                    break;
                case "Pillows":
                    spawnCoroutine = StartCoroutine(SpawnProjectiles());
                    break;
            }
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    public void SetSpawnRate(float newMinSpawnTime, float newMaxSpawnTime)
    {
        minSpawnTime = newMinSpawnTime;
        maxSpawnTime = newMaxSpawnTime;
    }

    // public void SetSpawnDuration(float duration)
    // {
    //     if (spawnCoroutine != null)
    //     {
    //         StopCoroutine(spawnCoroutine);
    //     }
    //     spawnCoroutine = StartCoroutine(SpawnForDuration(duration));
    // }

    // public void SetSpawnNumber(int numberOfProjectiles)
    // {
    //     StartCoroutine(SpawnMultipleProjectiles(numberOfProjectiles));
    // }

    // public void SetSpawnProjectile(GameObject chosenProjectilePrefab) //set chosen ONE projectile to spawn
    // {
    //     BuffProjectilePrefabs = new List<GameObject> { chosenProjectilePrefab };
    // }

    // public void SetSpawnProjectiles(List<GameObject> chosenProjectilePrefabs) //set chosen MULTIPLE projectiles to spawn
    // {
    //     BuffProjectilePrefabs = chosenProjectilePrefabs;
    // }

    // public void SetSpawnDirection(Direction newDirection)
    // {
    //     spawnDirection = newDirection;
    // }

    // public void SetSpawnLocation(Vector3 newLocation)
    // {
    //     spawnPoint.position = newLocation;
    // }

    public void SetProjectileSpeed(float newSpeed)
    {
        foreach (GameObject prefab in ProjectilePrefabs)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(newSpeed);
        }
        foreach (GameObject prefab in BuffPrefabs)
        {
            Projectile projectileScript = prefab.GetComponent<Projectile>();
            projectileScript.SetProjectileSpeed(newSpeed);
        }
    }

    public void SetMobSpeed(float newSpeed)
    {
        foreach (GameObject prefab in MobPrefabs)
        {
            prefab.GetComponent<Mob>().CurrentSpeed = newSpeed;
        }
    }

    public Quaternion GetSpawnDirection()
    {
        switch (spawnDirection)
        {
            case Direction.Up:
                return Quaternion.Euler(0, 0, 0);
            case Direction.Down:
                return Quaternion.Euler(0, 0, 180);
            case Direction.Left:
                return Quaternion.Euler(0, 0, 90);
            case Direction.Right:
                return Quaternion.Euler(0, 0, -90);
            default:
                return Quaternion.identity;
        }
    }

    public Vector3 GetSpawnPosition()
    {
        float randomDelta = UnityEngine.Random.Range(-maxDelta, maxDelta);
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 randomMovement;
        switch (spawnDirection)
        {
            case Direction.Up:
            case Direction.Down:
                randomMovement = new Vector2(randomDelta, 0);
                break;
            case Direction.Left:
            case Direction.Right:
                randomMovement = new Vector2(0, randomDelta);
                break;
            default:
                randomMovement = Vector2.zero;
                break;
        }
        Vector2 newPosition = currentPosition + randomMovement;
        return new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private IEnumerator SpawnProjectiles()
    {
        while (isSpawning)
        {
            GameObject projectileToSpawn = GetRandomBuffOrProjectileBasedOnChance();
            if (projectileToSpawn != null)
            {
                GameObject newProjectile = Instantiate(projectileToSpawn, GetSpawnPosition(), GetSpawnDirection());
                ProjectileManager.Instance.AddNewProjectile(newProjectile);
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    private IEnumerator SpawnMobs()
    {
        while (isSpawning)
        {
            GameObject mobToSpawn = GetRandomMobBasedOnChance();
            if (mobToSpawn != null)
            {
                GameObject newMob = Instantiate(mobToSpawn, GetSpawnPosition(), GetSpawnDirection());
                MobManager.Instance.AddNewMob(newMob);
            } 
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    // private IEnumerator SpawnForDuration(float duration)
    // {
    //     float elapsedTime = 0f;
    //     while (elapsedTime < duration)
    //     {
    //         Instantiate(BuffProjectilePrefabs[Random.Range(0, BuffProjectilePrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
    //         elapsedTime += Random.Range(minSpawnTime, maxSpawnTime);
    //         yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
    //     }
    //     StopSpawning();
    // }

    // private IEnumerator SpawnMultipleProjectiles(int numberOfProjectiles)
    // {
    //     for (int i = 0; i < numberOfProjectiles; i++)
    //     {
    //         Instantiate(BuffProjectilePrefabs[Random.Range(0, BuffProjectilePrefabs.Count)], GetSpawnPosition(), GetSpawnDirection());
    //         yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
    //     }
    // }

    public void OnDifficultyChanged(int newDifficulty)
    {
        SpawnerManager.Instance.AdjustDifficulty(newDifficulty);
        if(newDifficulty == 0) return;
        if(SceneManagerScript.Instance.sceneName == "Pillows")
        {    
            GameObject medkitPrefab = ProjectilePrefabs.Find(p => p.GetComponent<Projectile>() is Medkit);
            if (medkitPrefab != null)
            {
                GameObject newMedkit = Instantiate(medkitPrefab, GetSpawnPosition(), GetSpawnDirection());
                ProjectileManager.Instance.AddNewProjectile(newMedkit);
            }
        }
    }

    private GameObject GetRandomBuffOrProjectileBasedOnChance()
    {
        if (GameManager.Instance.spawnOnlyCoins)
        {
            OnlyCoinsSetUp();
            return ProjectilePrefabs.Find(p => p.GetComponent<Projectile>() is Coin);
        }
        OnlyCoinsReset();

        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if(randomValue < BuffChance)
        {
            return GetRandomBuff();
        }
        else
        {
            return GetRandomProjectile();
        }
    }

    GameObject GetRandomBuff()
    {
        int randomId = System.Convert.ToInt32(Mathf.Floor(UnityEngine.Random.Range(0f, BuffPrefabs.Count)));
        return BuffPrefabs[randomId];
    }

    GameObject GetRandomProjectile()
    {
        int randomId = System.Convert.ToInt32(Mathf.Floor(UnityEngine.Random.Range(0f, ProjectilePrefabs.Count)));
        return ProjectilePrefabs[randomId];
    }

    private GameObject GetRandomMobBasedOnChance()
    {
        float randValue = UnityEngine.Random.Range(0f, 1);

        List<GameObject> sortedMobPrefabs = MobPrefabs.OrderBy(prefab => prefab.GetComponent<Mob>().SpawnChance).ToList();
        foreach (var prefab in sortedMobPrefabs) 
        {
            float spawnChance = prefab.GetComponent<Mob>().SpawnChance;
            if (randValue <= spawnChance) 
            {
                return prefab;
            } 
            // else 
            // {
            //     prefab.GetComponent<Mob>().SpawnChance *= 2;
            //     increase chance of spawning this mob 
            // }
        }

        return sortedMobPrefabs.Last();
    }

    void OnlyCoinsSetUp(){
        // Store the original values if not already stored
        if (_originalMinSpawnTime == 0 && _originalMaxSpawnTime == 0 && _originalProjectileSpeed == 0)
        {
            _originalMinSpawnTime = minSpawnTime;
            _originalMaxSpawnTime = maxSpawnTime;
            if (ProjectilePrefabs.Count > 0)
            {
                Projectile projectileScript = ProjectilePrefabs[0].GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    _originalProjectileSpeed = projectileScript.GetCurrentSpeed();
                }
            }
        }

        // Change the spawn rate and projectile speed
        SetSpawnRate(0.1f, 1.0f); // Example values
        SetProjectileSpeed(3.0f); // Example value
    }

    void OnlyCoinsReset(){
        // Reset the values to their original state
        if (_originalMinSpawnTime != 0 && _originalMaxSpawnTime != 0 && _originalProjectileSpeed != 0)
        {
            SetSpawnRate(_originalMinSpawnTime, _originalMaxSpawnTime);
            SetProjectileSpeed(_originalProjectileSpeed);

            // Clear the original values
            _originalMinSpawnTime = 0;
            _originalMaxSpawnTime = 0;
            _originalProjectileSpeed = 0;
        }
    }
}