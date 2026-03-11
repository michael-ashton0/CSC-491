using UnityEngine;

public class CubeClick : MonoBehaviour
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // Left click(?)
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(1)) // Right click(?)
        {
            PlaceCube();
        }
    }

    void PlaceCube()
    {
        Vector3 spawnPos = transform.position + Vector3.up;

        GameObject newCube = Instantiate(gameObject, spawnPos, Quaternion.identity);
    }
}