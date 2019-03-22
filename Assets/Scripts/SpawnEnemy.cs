using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}


public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float waveTime = 30f;

    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManager gameManager;
    private GameOverScript gameOver;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;


    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameMaster").GetComponent<GameManager>();
        gameOver = GameObject.Find("GameMaster").GetComponent<GameOverScript>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get the index of the current wave, and check if it’s the last one.
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            // If so, calculate how much time passed since the last enemy spawn and whether it’s time to spawn an enemy. 
            //Here you consider two cases. If it’s the first enemy in the wave, you check whether timeInterval is bigger 
            //than timeBetweenWaves. Otherwise, you check whether timeInterval is bigger than this wave’s spawnInterval. 
            //In either case, you make sure you haven’t spawned all the enemies for this wave.
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            WaveDuration();
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies)
            {
                // If necessary, spawn an enemy by instantiating a copy of enemyPrefab. You also increase the enemiesSpawned count.
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)
                    Instantiate(waves[currentWave].enemyPrefab);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            // You check the number of enemies on screen. If there are none and it was the last enemy in the wave you spawn the next wave
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            gameOver.GameWon();
        }

    }

    IEnumerator WaveDuration()
    {
        if(waveTime > 0)
        {
            waveTime -= Time.deltaTime;
        }

        yield return null;
    }

}
