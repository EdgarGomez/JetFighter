using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource backgroundMusic;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public GameObject endGameGrid;

    public Image winnerShip;
    public Sprite player1Ship;
    public Sprite player2Ship;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("GameMode"));
        winnerShip.sprite = player1Ship;
    }

    public void GameOver(bool isPlayer1Winner)
    {
        Debug.Log(isPlayer1Winner);

        if (!isPlayer1Winner)
        {
            gameOverText.text = "Player 1 wins!";
            winnerShip.sprite = player1Ship;
        }
        else
        {
            gameOverText.text = "Player 2 wins!";
            winnerShip.sprite = player2Ship;
        }
        endGameGrid.SetActive(true);
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
