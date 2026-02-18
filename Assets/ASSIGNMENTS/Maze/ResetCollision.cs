using UnityEngine;

public class ResetCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var col = GetComponent<Collider>();
        col.enabled = true;
        print("Collision Reset");
    }
}
