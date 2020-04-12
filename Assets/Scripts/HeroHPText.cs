using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHPText : MonoBehaviour
{
    Text displayHP;
    Player player;

    void Start()
    {
        displayHP = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        displayHP.text = player.getHealth().ToString();
    }
}
