using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] GameObject pathPrefab = null;
    [Header("Cooldown")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [Header("Numer of Enemies & Speed")]
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    #region Get WaveConfig Info
    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
    #endregion

    #region Set WaveConfig Info
    public void SetEnemyPrefab(GameObject obj)
    {
        enemyPrefab = obj;
    }

    public void SetPathPrefab(GameObject obj)
    {
        pathPrefab = obj;
    }

    public void SetTimeBetweenSpawns(float num)
    {
        timeBetweenSpawns = num;
    }

    public void SetSpawnRandomFactor(float num)
    {
        spawnRandomFactor = num;
    }

    public void SetNumberOfEnemies(int num)
    {
        numberOfEnemies = num;
    }

    public void SetMoveSpeed(float num)
    {
        moveSpeed = num;
    }
    #endregion

}
