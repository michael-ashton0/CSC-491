using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Booooom singleton
    public static GameManager Instance;
    public GameObject gameOver;
    public GameObject Pause;
    public GameObject Resume;
    public GameObject StartMenu;
    public bool isGameOver;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighscoreText;

    private int score;
    private int highscore;
    
    List<GameObject> obstacles = new List<GameObject>();
    
    private void Awake()
    {
        // SINGLEton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        highscore = PlayerPrefs.GetInt("Highscore");
        ScoreText.text = score.ToString();
        HighscoreText.text = "Best: " + highscore.ToString();
    }

    private void Start()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        StartMenu.SetActive(false);
    }

    public void AddScore()
    {
        score++;

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }
        ScoreText.text = score.ToString();
        HighscoreText.text = "Best: " + highscore.ToString();
    }
    public void GameOver()
    {
        if (isGameOver) return;
        
        FindFirstObjectByType<FlappyAudio>().Play("Death");
        isGameOver = true;
        gameOver.SetActive(true);
        
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles.AddRange(pipes);

        float timer = 15f;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        foreach (GameObject pipe in obstacles)
        {
            pipe.transform.Translate(Vector2.left * 3 * Time.deltaTime);
        }
        
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindFirstObjectByType<FlappyAudio>().Play("Input");
    }

    public void PauseGame()
    {
        Pause.SetActive(false);
        Resume.SetActive(true);
        Time.timeScale = 0f;
        FindFirstObjectByType<FlappyAudio>().Play("Input");
    }

    public void ResumeGame()
    {
        Resume.SetActive(false);
        Pause.SetActive(true);
        Time.timeScale = 1f;
        FindFirstObjectByType<FlappyAudio>().Play("Input");
    }
}