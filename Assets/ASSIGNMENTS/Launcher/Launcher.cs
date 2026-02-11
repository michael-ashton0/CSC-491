using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Launcher : MonoBehaviour
{
    Rigidbody launcher;
    public float force = 10f;

    void Start()
    {
        launcher = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            launcher.AddForce(transform.up * force);
        }
    }
}