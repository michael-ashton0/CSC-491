using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // Booooom singleton
    public static GameManager Instance;
    private GameObject gameOver;

    [FormerlySerializedAs("IsGameOver")] public bool isGameOver;

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
    }
    
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Scenemanager doing a lot of heavy lifting
    }
}