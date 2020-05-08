using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] float score = 0;
    [Header("Enemy")]
    [SerializeField] int enemiesAlive = -1;
    [SerializeField] int waveNumber = 1;
    [Header("Enemy & Path Prefabs")]
    [SerializeField] List<GameObject> enemyPrefabs = null;
    [SerializeField] List<GameObject> pathPrefabs = null;
    [Header("Player")]
    [SerializeField] int levelDmg = 0;
    [SerializeField] float playerDmg = 100f;
    [SerializeField] int levelSpeed = 0;
    [SerializeField] float playerAtkSpeed = 0.75f;
    [SerializeField] int levelHp = 0;
    [SerializeField] float playerActualHp = 500f;
    [SerializeField] float playerMaxHp = 500f;

    GameObject upgradeCanvas = null;

    private void Start()
    {
        upgradeCanvas = GameObject.Find("PlayerUpgrade");
        upgradeCanvas.SetActive(false);
    }

    #region Get Game Stats

    public float GetPlayerDmg()
    {
        return playerDmg;
    }

    public float GetPlayerAtkSpeed()
    {
        return playerAtkSpeed;
    }

    public float GetPlayerActualHp()
    {
        return playerActualHp;
    }

    public void SetPlayerActualHp(float value)
    {
        playerActualHp = value;
    }

    public float GetPlayerMaxHp()
    {
        return playerMaxHp;
    }

    public List<int> GetLevels()
    {
        List<int> list = new List<int>();
        list.Add(levelDmg);
        list.Add(levelSpeed);
        list.Add(levelHp);

        return list;
    }

    public int GetEnemyWave()
    {
        return waveNumber;
    }

    public int GetEnemiesAlive()
    {
        return enemiesAlive;
    }
    #endregion

    #region Player Level Up
    public void AddLevelDamage() 
    { 
        bool valid;

        valid = SpendScore((levelDmg + 1) * 100);

        if(valid == true)
        {
            levelDmg++;
            playerDmg *= 1.03f;
        }
    }

    public void AddLevelSpeed()
    {
        bool valid;

        valid = SpendScore((levelSpeed + 1) * 100);

        if(valid == true)
        {
            levelSpeed++;
            playerAtkSpeed *= 0.95f;
        }
    }

    public void AddLevelHp()
    {
        bool valid;

        valid = SpendScore((levelHp + 1) * 100);

        if (valid == true)
        {
            levelHp++;
            playerMaxHp *= 1.1f;
            //Refresh Hp
            playerActualHp = playerMaxHp;
        }
    }
    #endregion

    #region Enemy
    public bool CheckIfEnemiesAlive()
    {
        if (upgradeCanvas)
        {
            if(enemiesAlive <= 0)
            {
                //End Wave. Appear UpgradeUI
                upgradeCanvas.SetActive(true);
                return false;
            }
            else
            {
                upgradeCanvas.SetActive(false);
                return true;
            }
        }
        else
        {
            Debug.LogError("Upgrade Canvas not found!!!");
            return false;
        }
    }

    public void killOneEnemy()
    {
        enemiesAlive -= 1;
    }

    public void SetEnemiesAlive(int count)
    {
        enemiesAlive = count;
    }

    public void AddWaveNumber()
    {
        waveNumber++;
    }
    #endregion

    #region Get Enemy & Path prefabs
    public List<GameObject> GetEnemyPrefabs()
    {
        return enemyPrefabs;
    }

    public List<GameObject> GetPathPrefabs()
    {
        return pathPrefabs;
    }
    #endregion

    #region Set Singleton

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    #endregion

    #region ScoreLogic

    public float GetScore()
    {
        return score;
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public bool SpendScore(int value)
    {
        if(value > score)
        {
            return false;
        }
        else
        {
            score -= value;
            return true;
        }
    }

    #endregion

    public GameObject GetUpgradeUI()
    {
        return upgradeCanvas;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
