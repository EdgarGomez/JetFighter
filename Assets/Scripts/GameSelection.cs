using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelection : MonoBehaviour
{

    private bool isAbout = false;
    public GameObject Buttons;
    public GameObject AboutText;
    public void difficultyLevel(int mode)
    {
        PlayerPrefs.SetInt("GameMode", mode);
        SceneManager.LoadScene("Game");
    }

    public void IsAbout()
    {
        isAbout = !isAbout;
        if (isAbout)
        {
            Buttons.SetActive(false);
            AboutText.SetActive(true);
        }
        else
        {
            Buttons.SetActive(true);
            AboutText.SetActive(false);
        }
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

