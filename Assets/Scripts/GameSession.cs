using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int score = 0;
    [SerializeField] int enemiesAlive = -1;

    GameObject upgradeCanvas = null;

    private void Start()
    {
        upgradeCanvas = GameObject.Find("PlayerUpgrade");
        upgradeCanvas.SetActive(false);
    }

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

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int value)
    {
        score += value;
    }

    public GameObject GetUpgradeUI()
    {
        return upgradeCanvas;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
