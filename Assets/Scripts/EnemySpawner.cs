using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    GameSession gameSession = null;
    List<GameObject> enemyPrefabs = null;
    List<GameObject> pathPrefabs = null;

    IEnumerator Start()
    {
        //Get GameSession Info
        gameSession = FindObjectOfType<GameSession>();
        enemyPrefabs = gameSession.GetEnemyPrefabs();
        pathPrefabs = gameSession.GetPathPrefabs();
        //Spawn All Wave (Group by group)
        yield return StartCoroutine(SpawnAllWaves());
        //Destroy Wave Object
        Destroy(gameObject);
	}
	
    private IEnumerator SpawnAllWaves()
    {
        //Create New List<WaveConfig> for the Coroutine (Based on Wave Number)
        List<WaveConfig> randomWave = new List<WaveConfig>();
        for(int i = 0; i < (2 + gameSession.GetEnemyWave()/5); i++) //Number of WaveConfigs: 2 + (wave/5)
        {
            WaveConfig waveConfig = ScriptableObject.CreateInstance("WaveConfig") as WaveConfig;
            waveConfig.SetEnemyPrefab(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            waveConfig.SetPathPrefab(pathPrefabs[Random.Range(0, pathPrefabs.Count)]);
            waveConfig.SetMoveSpeed(Random.Range(3f, 5f));
            waveConfig.SetTimeBetweenSpawns(Random.Range(0.3f, 1f));
            waveConfig.SetSpawnRandomFactor(Random.Range(0.2f, 0.3f));
            int baseNumEnemies = 4 + (gameSession.GetEnemyWave() / 6);
            waveConfig.SetNumberOfEnemies(Random.Range(baseNumEnemies - 1, baseNumEnemies + 2));

            randomWave.Add(waveConfig);
        }
        //setup in GameSession How Many enemies in Wave
        int count = 0;
        for(int i = 0; i < randomWave.Count; i++)
        {
            count += randomWave[i].GetNumberOfEnemies();
        }
        gameSession.SetEnemiesAlive(count);
        //Spawn All enemies in the wave
        for (int waveIndex = 0; waveIndex < randomWave.Count; waveIndex++)
            {
            var currentWave = randomWave[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            //Instantiate enemy
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            //Tell enemy wich path
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            //Wait next enemy CD
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
