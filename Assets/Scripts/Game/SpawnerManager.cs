using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance { get; private set; }

    protected List<Spawner> spawners = new List<Spawner>();
    [SerializeField] private TMP_Text waveText; 
    public List<GameObject> projectilePrefabs; // might be private and set in register spawner to each spawner
    [SerializeField] private GameObject[] bossPrefabs;
    private GameObject bossInstance;
    [HideInInspector] public int currentDifficulty;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
        // Subscribe to the OnDifficultyChanged event
        if (LevelProgress.Instance != null)
        {
            LevelProgress.Instance.OnDifficultyChanged += spawner.OnDifficultyChanged;
        }
    }

    public void StartSpawning(params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.StartSpawning();
        }
    }
    public void StopSpawning(params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.StopSpawning();
        }
    }

    public void SpawnWithDelay(float delayInSeconds, params Spawner[] selectedSpawners)
    {
        StopSpawning(selectedSpawners);
        StartCoroutine(SpawnWithDelayCoroutine(delayInSeconds, selectedSpawners));
    }

    private IEnumerator SpawnWithDelayCoroutine(float delayInSeconds, params Spawner[] selectedSpawners)
    {
        yield return new WaitForSeconds(delayInSeconds); // Wait for the specified duration
        StartSpawning(selectedSpawners); // Start spawning after delay
    }

    /*------------------Spawner managering------------------*/
    public void SetSpawnRate(float minSpawnTime, float maxSpawnTime, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnRate(minSpawnTime, maxSpawnTime);
        }
    }
    public void SetSpawnDuration(float duration, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnDuration(duration);
        }
    }
    public void SetSpawnNumber(int numberOfProjectiles, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnNumber(numberOfProjectiles);
        }
    }
    public void SetSpawnProjectile(GameObject projectilePrefab, params Spawner[] selectedSpawners) //set chosen ONE projectile to spawn
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnProjectile(projectilePrefab);
        }
    }
    public void SetSpawnProjectiles(List<GameObject> projectilePrefabs, params Spawner[] selectedSpawners) //set chosen MULTIPLE projectiles to spawn
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnProjectiles(projectilePrefabs);
        }
    }
    public void SetSpawnDirection(Direction newDirection, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnDirection(newDirection);
        }
    }
    public void SetSpawnLocation(Vector3 newLocation, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetSpawnLocation(newLocation);
        }
    }
    public void SetProjectileSpeed(float newSpeed, params Spawner[] selectedSpawners)
    {
        foreach (var spawner in selectedSpawners)
        {
            spawner.SetProjectileSpeed(newSpeed);
        }
    }
    /*------------------Difficulty Settings------------------*/
    public void AdjustSpawnRates(int newDifficulty)
    {
        foreach (var spawner in spawners)
        {
            float newMinSpawnTime = Mathf.Max(0.672376f, spawner.minSpawnTime - 0.01f * newDifficulty);
            float newMaxSpawnTime = Mathf.Max(1.76346f, spawner.maxSpawnTime - 0.01f * newDifficulty);

            spawner.SetSpawnRate(newMinSpawnTime, newMaxSpawnTime);
        }
    }
    public void AdjustProjectileSpeed(int newDifficulty)
    {
        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            float currentSpeed = projectileScript.GetCurrentSpeed();
            currentSpeed = Mathf.Min(5.0f, currentSpeed + 0.01f * newDifficulty);
            projectileScript.SetProjectileSpeed(currentSpeed + 0.01f * newDifficulty);
        }
    }
    public void AdjustDifficulty(int newDifficulty)
    {
        if (newDifficulty != currentDifficulty)
        {
            currentDifficulty = newDifficulty;
            if((newDifficulty + 1) % 2 == 0){
                BossPreparation();
                AnnounceBoss();
            } else {
                AdjustSpawnRates(newDifficulty);
                AdjustProjectileSpeed(newDifficulty);
                StartCoroutine(WaitEndOfTheWave());
            }
        }
    }

    IEnumerator WaitEndOfTheWave(){
        StopSpawning(spawners.ToArray());
        while(!ProjectileManager.Instance.NoSpawnedProjectiles()) 
        {
            Debug.Log("waiting");
            yield return null;
        }
        AnnounceWave();
        SpawnWithDelay(1.5f, spawners.ToArray());
    } 

    public void ResetSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.SetSpawnRate(2.0f, 3.0f);
        }

        List<GameObject> projectiles = ProjectileManager.Instance.getProjectilesPrefabs();
        foreach (GameObject projectile in projectiles)
        {
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            projectileScript.ResetSpeed();
        }

        AnnounceWave();
    }

    void AnnounceBoss(){
        waveText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -100f);
        MakeAnnouncement("BOSS"); 
    }

    void AnnounceWave(){
        waveText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 500f);
        MakeAnnouncement("Wave " + (currentDifficulty + 1));
    }

    void BossPreparation(){
        ProjectileManager.Instance.DestroyAllProjectiles();
        
        NinjaController ninjaController = GameManager.Instance.ninjaController;
        GameObject ninja = ninjaController.gameObject;
        LeanTween.move(ninja, new Vector3(0, -3, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        ninjaController.hitScript.changeModeToBossMode();

        
        bossInstance = Instantiate(bossPrefabs[0], new Vector3(0, 2.02f, 0), Quaternion.identity);

        // Set the initial alpha of the boss to 0
        SpriteRenderer bossRenderer = bossInstance.GetComponentInChildren<SpriteRenderer>();
        if (bossRenderer != null)
        {
            Color initialColor = bossRenderer.color;
            initialColor.a = 0f;
            bossRenderer.color = initialColor;

            // Use LeanTween to smoothly change the alpha to 1
            LeanTween.value(bossInstance, 0f, 1f, 1.0f)
                .setOnUpdate((float val) =>
                {
                    Color c = bossRenderer.color;
                    c.a = val;
                    bossRenderer.color = c;
                }).setEase(LeanTweenType.easeInOutQuad);
        }
        StopSpawning(spawners.ToArray());
    }

    public void ContinueGameAfterBoss(){
        AfterBossCleanUp();
        SpawnWithDelay(1f, spawners.ToArray());
    }

    public void AfterBossCleanUp(){
        ProjectileManager.Instance.DestroyAllProjectiles();
        if(bossInstance != null) Destroy(bossInstance);
        NinjaController ninjaController = GameManager.Instance.ninjaController;
        GameObject ninja = ninjaController.gameObject;
        if (ninja != null)
        {
            LeanTween.move(ninja, new Vector3(0, 0, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        }
        ninjaController.hitScript.changeModeToWaveMode();
        
    }

    private void MakeAnnouncement(string text){
        waveText.gameObject.SetActive(true); // Activate the text object
        Color initialColor = waveText.color; 
        waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Set initial transparency to 0
        waveText.text = text; // Set the text to display the wave number

        // Animate the transparency from 0 to 1 using LeanTween.value
        LeanTween.value(0f, 1f, 0.5f).setOnUpdate((float alpha) =>
        {
            waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        }).setOnComplete(() =>
        {
            // After reaching full transparency, wait for 0.2 seconds and animate back to 0
            LeanTween.value(1f, 0f, 0.5f).setDelay(1f).setOnUpdate((float alpha) =>
            {
                waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            }).setOnComplete(() =>
            {
                waveText.gameObject.SetActive(false); // Deactivate the text object after the animation
            });
        });
    }
}