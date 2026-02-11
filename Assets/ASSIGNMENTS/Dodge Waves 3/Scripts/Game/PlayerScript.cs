using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis =  Input.GetAxisRaw("Horizontal");
        float yAxis =  Input.GetAxisRaw("Vertical");
        yAxis = 0;
        if (xAxis != 0 || yAxis != 0)
        {
            transform.position += new Vector3(xAxis * speed, yAxis * speed, 0) * Time.deltaTime;
        }

        if (transform.position.x < -5)
        {
            transform.position = new Vector3(-5, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 5)
        {
            transform.position = new Vector3(5, transform.position.y, transform.position.z);
        }
    }
}
