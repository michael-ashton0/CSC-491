using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleScript : MonoBehaviour
{
    public int wave;
    public Vector3 velocity;
    private PlayerScript player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wave = 1;
        transform.position = new Vector3(0, -5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < player.transform.position.y - 3)
        {
            transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(6, 10), 0.0f);
        }
        
        velocity += Vector3.down * 9.81f * Time.fixedDeltaTime;
        transform.position += velocity * Time.fixedDeltaTime;
        
        if (wave > 1 && Random.Range(0,1) > 0.5f)
        {
            transform.position = new Vector3(Mathf.Sin(Time.fixedTime), transform.position.y, transform.position.z);
        }
        
        if (Vector3.Distance(transform.position, player.transform.position) < 1)
        {
            Time.timeScale = 0;
            print($"Game Over, you made it to wave {wave}");
        }
    }
}
