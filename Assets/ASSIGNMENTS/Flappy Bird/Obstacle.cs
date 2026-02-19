using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Obstacle : MonoBehaviour
{
    List<GameObject> obstacles = new List<GameObject>();
    public TextMeshProUGUI scoreText;
    private Dictionary<GameObject, bool> pipeScored = new Dictionary<GameObject, bool>();
    public float speed = 3f;
    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Obstacle");
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        
        obstacles.AddRange(pipes);
        foreach (GameObject pipe in obstacles)
        {
            pipe.transform.position = new Vector2(pipe.transform.position.x, Random.Range(-2f, 2));
            pipeScored[pipe] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dx = speed * Time.deltaTime;
        
        foreach (GameObject pipe in obstacles)
        {
            pipe.transform.Translate(Vector2.right * dx);
            if (pipe.transform.position.x > 12)
            {
                float newY = Random.Range(-2f, 2f);
                pipe.transform.position = new Vector2(-12, newY);
                pipeScored[pipe] = false;
            }
            if (pipe.transform.position.x > 0 && !pipeScored[pipe])
            {
                score++;
                pipeScored[pipe] = true;
            }
        }
        scoreText.text = score.ToString();
    }
}
