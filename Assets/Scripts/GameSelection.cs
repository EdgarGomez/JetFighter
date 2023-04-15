using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{
    public void PlayerVsIA()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        SceneManager.LoadScene("Game");
    }

    public void PlayerVsPlayer()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        SceneManager.LoadScene("Game");
    }

    public void ObstaclesMode()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

