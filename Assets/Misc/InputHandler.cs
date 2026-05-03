using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float speed = 2f;
    private GameObject[] obstacles;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Ball");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        
        if (xAxis != 0 || yAxis != 0)
        {
            var dir = new Vector3(xAxis, yAxis,0).normalized;
            transform.position += dir * Time.fixedDeltaTime * speed;
        }
        
        foreach (var obstacle in obstacles)
        {
            if (Vector3.Distance(obstacle.transform.position, transform.position) < 1)
            {
                print("You Lose (again?)");
            }
        }
    }
}
