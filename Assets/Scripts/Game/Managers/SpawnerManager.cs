using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveText; 
    [SerializeField] private List<GameObject> _projectilePrefabs;
    [SerializeField] private List<GameObject> _buffPrefabs;
    [SerializeField] private List<GameObject> _mobPrefabs;
    [SerializeField] private List<GameObject> _bossPrefabs;
    [SerializeField] private float BuffChance;

    public static SpawnerManager Instance { get; private set; }

    protected List<Spawner> spawners = new List<Spawner>();
    private GameObject _bossInstance;
    [HideInInspector] public int CurrentDifficulty;

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
        spawner.ProjectilePrefabs = _projectilePrefabs;
        spawner.MobPrefabs = _mobPrefabs;
        spawner.BuffPrefabs = _buffPrefabs;
        spawner.BuffChance = BuffChance;
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
    // public void SetSpawnRate(float minSpawnTime, float maxSpawnTime, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnRate(minSpawnTime, maxSpawnTime);
    //     }
    // }
    // public void SetSpawnDuration(float duration, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnDuration(duration);
    //     }
    // }
    // public void SetSpawnNumber(int numberOfProjectiles, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnNumber(numberOfProjectiles);
    //     }
    // }
    // public void SetSpawnProjectile(GameObject projectilePrefab, params Spawner[] selectedSpawners) //set chosen ONE projectile to spawn
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnProjectile(projectilePrefab);
    //     }
    // }
    // public void SetSpawnProjectiles(List<GameObject> P, params Spawner[] selectedSpawners) //set chosen MULTIPLE projectiles to spawn
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnProjectiles(P);
    //     }
    // }
    // public void SetSpawnDirection(Direction newDirection, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnDirection(newDirection);
    //     }
    // }
    // public void SetSpawnLocation(Vector3 newLocation, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetSpawnLocation(newLocation);
    //     }
    // }
    // public void SetProjectileSpeed(float newSpeed, params Spawner[] selectedSpawners)
    // {
    //     foreach (var spawner in selectedSpawners)
    //     {
    //         spawner.SetProjectileSpeed(newSpeed);
    //     }
    // }
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

    public void AdjustMobsSpeed(int newDifficulty)
    {
        foreach (GameObject mobPrefab in _mobPrefabs)
        {
            mobPrefab.GetComponent<Mob>().CurrentSpeed += 0.01f * newDifficulty;
        }
    }

    public void AdjustDifficulty(int newDifficulty)
    {
        if (newDifficulty != CurrentDifficulty)
        {
            CurrentDifficulty = newDifficulty;
            switch (SceneManagerScript.Instance.sceneName)
            {
                case "Arcade":
                    if((newDifficulty + 1) % 3 == 0)
                    {
                        BossPreparation();
                        AnnounceBoss();
                    }
                    else
                    {
                        StartCoroutine(WaitEndOfTheWave());
                        AdjustSpawnRates(newDifficulty);
                        AdjustMobsSpeed(newDifficulty);
                    }
                    break;
                case "Pillows":
                    AdjustSpawnRates(newDifficulty);
                    AdjustProjectileSpeed(newDifficulty);
                    break;
            }
        }
    }

    IEnumerator WaitEndOfTheWave(){
        StopSpawning(spawners.ToArray());
        while(!MobManager.Instance.NoSpawnedMobs()) 
        {
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

        foreach (GameObject prefab in _mobPrefabs)
        {
            prefab.GetComponent<Mob>().ResetSpeed();
        }

        if(SceneManagerScript.Instance.sceneName == "Arcade")
            AnnounceWave();
    }

    void AnnounceBoss(){
        _waveText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -100f);
        MakeAnnouncement("BOSS"); 
    }

    void AnnounceWave(){
        _waveText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 500f);
        MakeAnnouncement("Wave " + (CurrentDifficulty + 1));
    }

    void BossPreparation(){
        ProjectileManager.Instance.RemoveAllProjectiles();
        MobManager.Instance.RemoveAllMobs();
        
        GameObject ninja = NinjaController.Instance.gameObject;
        LeanTween.move(ninja, new Vector3(0, -3, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        NinjaController.Instance.HitScr.ChangeModeToBossMode();

        _bossInstance = Instantiate(_bossPrefabs[0], new Vector3(0, 2.02f, 0), Quaternion.identity);

        // Set the initial alpha of the boss to 0
        SpriteRenderer bossRenderer = _bossInstance.GetComponentInChildren<SpriteRenderer>();
        if (bossRenderer != null)
        {
            Color initialColor = bossRenderer.color;
            initialColor.a = 0f;
            bossRenderer.color = initialColor;

            // Use LeanTween to smoothly change the alpha to 1
            LeanTween.value(_bossInstance, 0f, 1f, 1.0f)
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
        AnnounceWave();
        SpawnWithDelay(1f, spawners.ToArray());
    }

    public void AfterBossCleanUp(){
        ProjectileManager.Instance.DestroyAllProjectiles();
        MobManager.Instance.DestroyAllMobs();
        if (_bossInstance != null) Destroy(_bossInstance);
        GameObject ninja = NinjaController.Instance.gameObject;
        if (ninja != null)
        {
            LeanTween.move(ninja, new Vector3(0, 0, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        }
        NinjaController.Instance.HitScr.ChangeModeToWaveMode();
        
    }

    private void MakeAnnouncement(string text){
        _waveText.gameObject.SetActive(true); // Activate the text object
        Color initialColor = _waveText.color; 
        _waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0); // Set initial transparency to 0
        _waveText.text = text; // Set the text to display the wave number

        // Animate the transparency from 0 to 1 using LeanTween.value
        LeanTween.value(0f, 1f, 0.5f).setOnUpdate((float alpha) =>
        {
            _waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
        }).setOnComplete(() =>
        {
            // After reaching full transparency, wait for 0.2 seconds and animate back to 0
            LeanTween.value(1f, 0f, 0.5f).setDelay(1f).setOnUpdate((float alpha) =>
            {
                _waveText.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            }).setOnComplete(() =>
            {
                _waveText.gameObject.SetActive(false); // Deactivate the text object after the animation
            });
        });
    }
}
