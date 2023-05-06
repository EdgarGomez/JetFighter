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
    public bool isGameOver = false;
    public int difficultyLevel = 0; // 0 = easy, 1 = normal, 2 = hard
    public string[] objectTags;

    private bool escPressed = false;
    public bool isPaused = false;
    public GameObject pausePanel;


    void Awake()
    {
        Time.timeScale = isPaused ? 0 : 1;
        if (PlayerPrefs.HasKey("GameMode")) difficultyLevel = PlayerPrefs.GetInt("GameMode");
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
        if (PlayerPrefs.HasKey("GameMode")) difficultyLevel = PlayerPrefs.GetInt("GameMode");
        Debug.Log(difficultyLevel);
        winnerShip.sprite = player1Ship;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPressed)
            {
                if (!isGameOver)
                {
                    isPaused = !isPaused;
                    Time.timeScale = isPaused ? 0 : 1;
                    pausePanel.SetActive(isPaused);
                    escPressed = true;
                }
            }
            else
            {
                escPressed = false;
            }
        }
    }

    public void PauseOn()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void GameOver(bool isPlayer1Winner)
    {
        isGameOver = true;
        DestroyObjects();
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

    public void DestroyObjects()
    {
        // Iterate through each tag in the objectTags array
        foreach (string tag in objectTags)
        {
            // Find all game objects with the current tag
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

            // Iterate through the tagged objects and destroy them
            foreach (GameObject obj in taggedObjects)
            {
                Destroy(obj);
            }
        }
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
