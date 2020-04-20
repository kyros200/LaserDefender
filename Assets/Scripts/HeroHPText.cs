using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHPText : MonoBehaviour
{
    Text displayHP;
    GameSession gameSession;

    void Start()
    {
        displayHP = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        displayHP.text = gameSession.GetPlayerActualHp().ToString();
    }
}
