using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    public Transform respawnPoint;
    public float disableTime = 100f;
    
    private void OnTriggerEnter(Collider player)
    {
        var rb = player.GetComponent<Rigidbody>();
        var col = player.GetComponent<Collider>();
        
        col.enabled = false;
        print("inactive");
        // while (disableTime > 0)
        // {
        //     disableTime -= Time.deltaTime;
        //     print("disable time: " + disableTime);
        // }
        //
        // if (disableTime <= 0)
        // {
        //     col.enabled = true;
        //     print("active");
        // }
        
    }

    private void OnTriggerExit(Collider player)
    {
        var col = player.GetComponent<Collider>();
        col.enabled = true;
        print("exited");
    }
}