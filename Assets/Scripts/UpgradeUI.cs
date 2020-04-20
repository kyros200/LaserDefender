using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] GameObject spawner = null;

    public void NextWave()
    {
        FindObjectOfType<GameSession>().AddWaveNumber();
        Instantiate(spawner, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
