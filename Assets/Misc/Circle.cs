using Unity.Mathematics;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public float radius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(math.cos(Time.time), math.sin(Time.time), 0) * radius;
    }
}
