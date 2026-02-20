using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // Booooom singleton
    public static GameManager Instance;
    public GameObject gameOver;
    public GameObject Pause;
    public GameObject Resume;

    public bool isGameOver;

    private void Awake()
    {
        // SINGLEton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        FindFirstObjectByType<FlappyAudio>().Play("Input");
    }
    
    public void GameOver()
    {
        if (isGameOver) return;
        
        FindFirstObjectByType<FlappyAudio>().Play("Death");
        
        isGameOver = true;
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Scenemanager doing a lot of heavy lifting
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