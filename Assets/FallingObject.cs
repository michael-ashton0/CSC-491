using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FallingObject : MonoBehaviour
{
    private Vector3 velocity;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        velocity += Vector3.down * 9.81f * Time.fixedDeltaTime;
        
        transform.position += velocity * Time.fixedDeltaTime;

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(Random.Range(-5, 5), Random.Range(3, 5), transform.position.z);
            velocity = Vector3.zero;
        }
    }
}
