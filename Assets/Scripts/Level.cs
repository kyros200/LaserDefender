using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    
    public void LoadGameOver()
    {
        StartCoroutine(DelayToChangeScene());
    }

    private IEnumerator DelayToChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameScene()
    {
        if(FindObjectsOfType<GameSession>().Length > 0)
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
        SceneManager.LoadScene("MainGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
