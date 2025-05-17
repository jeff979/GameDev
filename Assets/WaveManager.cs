using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public GameObject skullPodPrefab;
    public Transform player;
    public LayerMask groundMask;

    public float waveDelay = 15f;
    public int enemiesPerWave = 16;
    public float dropRadius = 50f;
    public float dropHeight = 120f;
    private int waveNumber = 1;

    private float waveTimer = 0f;
    private bool waveInProgress = false;
    private bool firstWaveStarted = false;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        activeEnemies.RemoveAll(e => e == null);
        waveTimer += Time.deltaTime;

        if (waveInProgress)
        {
            if (activeEnemies.Count == 0)
            {
                waveInProgress = false;
            }

            else if (waveTimer >= waveDelay)
            {
                SpawnWave();
                waveTimer = 0f;
                waveInProgress = true;
            }
        }
        else
        {
            if (!firstWaveStarted)
            {
                SpawnWave();
                waveInProgress = true;
                firstWaveStarted = true;
                waveTimer = 0f;
            }
            else if (waveTimer >= waveDelay)
                {
                    SpawnWave();
                    waveInProgress = true;
                     waveTimer = 0f;
            }
        }
    }

    // spawns 1 million enemies ????

    //void Update()
    //{
    //    activeEnemies.RemoveAll(e => e == null);
    //    bool allEnemiesCleared = activeEnemies.Count == 0;

    //    if (!waveInProgress)
    //    {
    //        waveTimer += Time.deltaTime;

    //        if (!firstWaveStarted || waveTimer >= waveDelay || allEnemiesCleared)
    //        {
    //            waveInProgress = true;
    //            firstWaveStarted = true;
    //            waveTimer = 0f;
    //            SpawnWave();
    //        }
    //    }

    //    if (waveInProgress && allEnemiesCleared)
    //    {
    //        waveTimer = 0f;
    //        waveInProgress = false;
    //    }
    //}

        void SpawnWave()
    {
        Debug.Log("Wave " + waveNumber + " started with " + enemiesPerWave + " enemies.");
        waveNumber++;
        int podsToSpawn = Mathf.CeilToInt(enemiesPerWave / 4f); 
        for (int i = 0; i < podsToSpawn; i++)
        {
            Vector3 offset = Random.insideUnitSphere * dropRadius;
            offset.y = 0;
            Vector3 spawnPos = player.position + offset + Vector3.up * dropHeight;

            GameObject pod = Instantiate(skullPodPrefab, spawnPos, Quaternion.identity);

            SkullPod podScript = pod.GetComponent<SkullPod>();
            if (podScript != null)
            {
                podScript.StartFalling();
                podScript.Initialise(this, player);
            }
        }

        enemiesPerWave += 4;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        activeEnemies.Add(enemy);
    }
}