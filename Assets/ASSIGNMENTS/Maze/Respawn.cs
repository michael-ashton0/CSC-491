using System;
using Unity.VisualScripting;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnPoint; 
    
    void OnTriggerEnter(Collider other)
    {
        print("Collision Enter");
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        print("Collision Enter");
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.transform.position;
        }
    }
}