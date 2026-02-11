using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using TMPro;

public class ManagerScript : MonoBehaviour
{
    public GameObject[] obstacles;
    private GameObject player;

    public List<GameObject> obstaclesList = new List<GameObject>();

    private int ballCount = 6;
    public static int counter;

    public int wave = 1;
    public int score = 0;

    public TextMeshProUGUI score_text;
    public TextMeshProUGUI wave_counter;

    private bool isGameOver = false;

    // only score each obstacle once - idea from chat
    private HashSet<int> countedObstacleIds = new HashSet<int>();

    // also asked chat about a refactor since my code was getting ridiculous
    // just ended up adding more functions
    void Start()
    {
        obstaclesList.Clear();
        
        obstacles = GameObject.FindGameObjectsWithTag("Ball");
        player = GameObject.FindGameObjectWithTag("Player");

        obstaclesList.AddRange(obstacles);
        obstaclesList.AddRange(GameObject.FindGameObjectsWithTag("Slider"));
        ResetGame();
    }

    private void ResetGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        score = 0;
        wave = 1;
        counter = 0;
        countedObstacleIds.Clear();

        UpdateUI();
        
        foreach (GameObject obstacle in obstaclesList)
        {
            float x = Random.Range(-4.5f, 4.5f);
            float y = Random.Range(-4f, -2f);
            obstacle.transform.position = new Vector3(x, y, 0f);
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        print("Lost");
    }

    void Update()
    {
        if (isGameOver && Input.GetButtonDown("Jump"))
        {
            ResetGame();
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        foreach (GameObject obstacle in obstaclesList)
        {
            if (Vector3.Distance(player.transform.position, obstacle.transform.position) < 1f)
            {
                GameOver();
                return;
            }
            if (obstacle.transform.position.y < player.transform.position.y - 9f)
            {
                int id = obstacle.GetInstanceID();
                if (!countedObstacleIds.Contains(id))
                {
                    countedObstacleIds.Add(id);

                    counter++;
                    score++;
                    UpdateUI();
                }
            }
            
            float y = obstacle.transform.position.y;
            float x = Mathf.Sin(Time.time + y + (obstacle.GetInstanceID() / 10023123f)) * 10f;
            obstacle.transform.position = new Vector3(x, y, 0f);
            obstacle.transform.position += Vector3.down * 2f * Time.fixedDeltaTime;
        }
        
        if (counter >= ballCount)
        {
            wave++;
            counter = 0;
            countedObstacleIds.Clear(); // prep for next wave

            foreach (GameObject obs in obstaclesList)
            {
                obs.transform.position = new Vector3(
                    Random.Range(-4.5f, 4.5f),
                    Random.Range(7f, 15f),
                    0f
                );
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (score_text != null) score_text.text = score.ToString();
        if (wave_counter != null) wave_counter.text = wave.ToString();
    }
}
