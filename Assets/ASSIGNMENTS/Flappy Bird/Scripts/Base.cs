using UnityEngine;

public class Base : MonoBehaviour
{
    public GameObject Ground;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Ground.transform.position.x > 10f)
        {
            Ground.transform.position = new Vector2(-16f, Ground.transform.position.y);
        }
    }
}
