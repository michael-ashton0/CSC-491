using UnityEngine;

public class CubeGridSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int width = 5;
    public int height = 5;
    public float spacing = 1f;

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 spawnPos = new Vector3(
                    x * spacing - (width - 1) * spacing / 2f,
                    0f,
                    z * spacing - (height - 1) * spacing / 2f
                );
                Instantiate(cubePrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }
}