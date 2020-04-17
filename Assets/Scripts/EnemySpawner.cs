using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs = null;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

	IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
        Destroy(gameObject);
	}
	
    private IEnumerator SpawnAllWaves()
    {
        //setup GameSession
        int count = 0;
        for(int i = 0; i < waveConfigs.Count; i++)
        {
            count += waveConfigs[i].GetNumberOfEnemies();
        }
        FindObjectOfType<GameSession>().SetEnemiesAlive(count);
        //Spawn All enemies in the wave
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
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
