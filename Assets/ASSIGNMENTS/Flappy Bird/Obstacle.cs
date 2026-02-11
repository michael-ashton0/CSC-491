using UnityEngine;
using System.Collections.Generic;
public class Obstacle : MonoBehaviour
{
    List<GameObject> obstacles = new List<GameObject>();
    
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
            pipe.transform.Translate(Vector3.right * Time.deltaTime * 3);
        }
        
    }
}
