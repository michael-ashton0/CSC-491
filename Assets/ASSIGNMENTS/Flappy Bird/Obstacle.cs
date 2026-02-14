using UnityEngine;
using System.Collections.Generic;
public class Obstacle : MonoBehaviour
{
    List<GameObject> obstacles = new List<GameObject>();

    public int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles.AddRange(pipes);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject pipe in obstacles)
        {
            pipe.transform.Translate(Vector2.right * Time.deltaTime * 3);
            if (pipe.transform.position.x > 10)
            {
                pipe.transform.position = new Vector2(-5, pipe.transform.position.y * Random.Range(1.0f,1.5f));
            }
            if (pipe.transform.position.x == 0)
            {
                score++;
            }
        }
    }
}
