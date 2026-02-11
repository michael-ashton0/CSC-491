using UnityEngine;
using System.Threading;
public class CubeMover : MonoBehaviour
{
    public float WaitTime;
    public float Speed;
    public float Min_X;
    public float Max_X;
    
    private bool movingRight = true;
    private float timer;
    void Start()
    {
        // transform.position = new Vector3(3.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        // Clunky, but thread.sleep was super jerky
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        
        // Pick direction
        if (transform.position.x >= Max_X)
        {
            transform.position = new Vector3(Max_X, transform.position.y, transform.position.z);
            movingRight = false;
            timer = WaitTime;
        }
        else if (transform.position.x <= Min_X)
        {
            transform.position = new Vector3(Min_X, transform.position.y, transform.position.z);
            movingRight = true;
            timer = WaitTime;
        }
        
        // Move
        if (movingRight == true)
        {
            transform.position += Time.deltaTime * Speed * new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            transform.position -= Time.deltaTime * Speed * new Vector3(1.0f, 0.0f, 0.0f);
        }
    }
}
