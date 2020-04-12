using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text displayScore;
    
    void Start()
    {
        displayScore = GetComponent<Text>();
    }

    void Update()
    {
        displayScore.text = FindObjectOfType<GameSession>().GetScore().ToString();
    }
}
