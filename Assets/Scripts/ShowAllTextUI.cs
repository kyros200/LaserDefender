using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAllTextUI: MonoBehaviour
{
    [Header("Price Texts")]
    [SerializeField] Text actualScore = null;
    [SerializeField] Text DmgText = null;
    [SerializeField] Text SpeedText = null;
    [SerializeField] Text HpText = null;
    [Header("Player Info")]
    [SerializeField] Text gameplayHp = null;
    [SerializeField] Text allPlayerInfo = null;
    [Header("Enemy Info")]
    [SerializeField] Text enemyWave = null;

    GameSession gameSession = null;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void Update()
    {
        //Text During Gameplay (HP and ActualScore)
        actualScore.text = "$:" + gameSession.GetScore().ToString("F2");
        gameplayHp.text = "HP:" + gameSession.GetPlayerActualHp().ToString("F2") + "\n(" + gameSession.GetPlayerMaxHp().ToString("F2") + ")";
        enemyWave.text = "Wave:" + gameSession.GetEnemyWave().ToString() + "\n Alive:" + gameSession.GetEnemiesAlive().ToString();
        //Upgrade Prices
        List<int> list = gameSession.GetLevels();
        DmgText.text = "$: " + ((list[0] + 1) * 100).ToString();
        SpeedText.text = "$: " + ((list[1] + 1) * 100).ToString();
        HpText.text = "$: " + ((list[2] + 1) * 100).ToString();
        //Player Info During Upgrades
        string playerInfo = "";
        playerInfo += "==PLAYER STATUS==\n";
        playerInfo += "Dmg Level " + list[0] + "\n" + gameSession.GetPlayerDmg().ToString("F2") + " (" + (gameSession.GetPlayerDmg() * 1.03).ToString("F2") + ")\n";
        playerInfo += "Speed Level " + list[1] + "\n" + gameSession.GetPlayerAtkSpeed().ToString("F2") + " seg (" + (gameSession.GetPlayerAtkSpeed() * 0.95).ToString("F2") + " seg)\n";
        playerInfo += "HP Level " + list[2] + "\n" + gameSession.GetPlayerMaxHp().ToString("F2") + " (" + (gameSession.GetPlayerMaxHp() * 1.1).ToString("F2") + ")\n";
        allPlayerInfo.text = playerInfo;
    }

}
