using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] Text finalScore = null;

    private void Start()
    {
        finalScore.text = FindObjectOfType<GameSession>().GetScore().ToString("F2");
    }
}
