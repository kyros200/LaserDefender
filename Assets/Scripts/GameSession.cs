﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int score = 0;
    [Header("Enemy")]
    [SerializeField] int enemiesAlive = -1;
    [SerializeField] int waveNumber = 1;
    [Header("Player")]
    [SerializeField] int levelDmg = 0;
    [SerializeField] float playerDmg = 100f;
    [SerializeField] int levelSpeed = 0;
    [SerializeField] float playerAtkSpeed = 10f;
    [SerializeField] int levelHp = 0;
    [SerializeField] float playerActualHp = 500f;
    [SerializeField] float playerMaxHp = 500f;

    GameObject upgradeCanvas = null;

    private void Start()
    {
        upgradeCanvas = GameObject.Find("PlayerUpgrade");
        upgradeCanvas.SetActive(false);
    }

    #region Get Player Stats

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

    #endregion

    #region Player Level Up
    public void AddLevelDamage() 
    { 
        bool valid;

        valid = SpendScore(levelDmg* 100);

        if(valid == true)
        {
            levelDmg++;
            playerDmg = 100f * Mathf.Pow(1.03f, (float) levelDmg);
        }
    }

    public void AddLevelSpeed()
    {
        bool valid;

        valid = SpendScore(levelSpeed * 100);

        if(valid == true)
        {
            levelSpeed++;
            playerAtkSpeed = 1f * Mathf.Pow(0.95f, (float)levelSpeed);
        }
    }


    public void AddLevelHp()
    {
        bool valid;

        valid = SpendScore(levelHp * 100);

        if (valid == true)
        {
            levelHp++;
            playerMaxHp = 500f * Mathf.Pow(1.1f, (float)levelHp);
            //Refresh Hp
            playerActualHp = playerMaxHp;
        }
    }
    #endregion

    #region Enemy
    public bool CheckIfEnemiesAlive()
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

    #region ScoreUI & ScoreLogic

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int value)
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
