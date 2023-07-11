
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float spawnDelay = 0.3f;
    [SerializeField] private float waveDelay = 10f;
    [SerializeField] private int minEnemiesInWave = 3;
    [SerializeField] private int maxEnemiesInWave = 8;
    [SerializeField] private int totalAmountOfWaves = 5;
    [SerializeField] private TMP_Text enemiesAliveText;
    private int currentWave = 0;
    public uint enemiesAlive = 0;
    public static event Action GameOver = delegate{};

    private static EnemyManager instance;
    public static EnemyManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }
    

    void Start()
    {
        UpdateEnemiesAliveText();

        StartCoroutine(nameof(SpawnWaves));
    }

    private IEnumerator SpawnEnemy()
    {
        int enemiesInWave = Random.Range(minEnemiesInWave, maxEnemiesInWave);
        for (int i = 0; i < enemiesInWave; i++)
        {
            Instantiate(enemyPrefab,spawnLocations[Random.Range(0, spawnLocations.Length)].position, Quaternion.identity, enemyParent );
            enemiesAlive++;
            UpdateEnemiesAliveText();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < totalAmountOfWaves)
        {
            currentWave++;
            StartCoroutine(nameof(SpawnEnemy));
            yield return new WaitForSeconds(waveDelay);
        }
    }
    
    public void UpdateEnemiesAliveText()
    {
        enemiesAliveText.text = enemiesAlive.ToString();
    }
    
    public void EnemyKilled()
    {
        enemiesAlive--;
        enemiesAliveText.text = enemiesAlive.ToString();
        if (enemiesAlive <= 0 && currentWave == totalAmountOfWaves)
        {
            // You have killed all the enemies. Threat averted
            GameOver();
        }
    }
    
    public void DestroyThyself()
    {
        Destroy(gameObject);
        instance = null;    // because destroy doesn't happen until end of frame
    }
}
