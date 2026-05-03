using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SphereCollision : MonoBehaviour
{
    
    public List<GameObject> colliders;
    public float speed = 5.0f;

    public float direction = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    GameObject[] _spheres = GameObject.FindGameObjectsWithTag("Ball");
    GameObject[] _walls = GameObject.FindGameObjectsWithTag("Wall");
    
    colliders.AddRange(_spheres);
    colliders.AddRange(_walls);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var col in colliders)
        {
            if (col == gameObject)
            {
                continue;
            }
            
            var distance = Vector3.Distance(col.transform.position, transform.position);
            if (distance < 1)
            {
                direction *= -1;
            }
        }
        transform.position += new Vector3(speed * direction, 0, 0) * Time.fixedDeltaTime;
        
    }
}
