//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
//    public GameObject enemyPrefab; 
//    public Transform player; 
//    public float spawnRadius = 40f;
//    public float spawnHeight = 35f; 
//    public int maxEnemies = 8; 
//    public float spawnRate = 2f;

//    private int currentEnemyCount = 0;

//    void Start()
//    {
//        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
//    }

//    void SpawnEnemy()
//    {
//        if (currentEnemyCount >= maxEnemies) return; 

        
//        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
//        spawnPosition.y = player.position.y + spawnHeight;

//        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
//        //enemy.GetComponent<EnemyController>().StartFalling(); 
//        currentEnemyCount++;
//    }

//    public void EnemyDied()
//    {
//        currentEnemyCount--; 
//    }
//}