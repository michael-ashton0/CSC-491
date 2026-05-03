using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGeneration : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject stonePrefab;
    public GameObject coalPrefab;
    public GameObject dirtPrefab;
    public GameObject logPrefab;
    public GameObject leafPrefab;
    
    public int width = 5;
    public int height = 5;
    public float spacing = 1f;

    public float treeChance;
    public float treeDistance;
    
    public int terrain_height_scaler = 3;
    public int tree_height_scaler = 5;
    
    private List<Vector3> treePositions = new List<Vector3>();
    
    void Start()
    {
        SpawnTopLayer();
    }

    void SpawnTopLayer()
    {
        if (treeDistance < 4)
        {
            treeDistance = 4;
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float chances = Random.Range(0f, 1f);
                float spawn_x = x * spacing - (width - 1) * spacing / 2f;
                float spawn_z = z * spacing - (height - 1) * spacing / 2f;
                
                float tmp_height = Mathf.PerlinNoise(spawn_x, spawn_z);
                tmp_height *= terrain_height_scaler;
                int spawn_height = Mathf.RoundToInt(tmp_height);
                
                Vector3 spawnPos = new Vector3(spawn_x, spawn_height, spawn_z);
                Instantiate(grassPrefab, spawnPos, Quaternion.identity, transform);
                SpawnColumnBelow(spawnPos, 3);
                
                if (chances <= treeChance && CanPlaceTree(spawnPos))
                {
                    treePositions.Add(spawnPos);
                    AddTree(spawnPos);
                }
                
            }
        }
    }

    void SpawnColumnBelow(Vector3 spawnPos, int layerSize)
    {
        Vector3 newSpawn = spawnPos;
        for (int i = 0; i < layerSize; i++)
        {
            newSpawn += Vector3.down;
            Instantiate(dirtPrefab, newSpawn,  Quaternion.identity, transform);
        }
        for (int i = 0; i < layerSize; i++)
        {
            newSpawn += Vector3.down;
            Instantiate(stonePrefab, newSpawn,  Quaternion.identity, transform);
        }
    }

    void AddTree(Vector3 spawnPos)
    {
        Vector3 newSpawn = spawnPos;

        float tree_heightf = Mathf.PerlinNoise(spawnPos.x, spawnPos.z) * tree_height_scaler;
        int tree_height = Mathf.RoundToInt(tree_heightf);

        tree_height += 4; //ensuring at least some ground clearance

        for (int i = 0; i < tree_height; i++)
        {
            newSpawn += Vector3.up;
            Instantiate(logPrefab, newSpawn, Quaternion.identity, transform);
        }

        int[] sizes = { 3, 2, 1 };

        for (int y = 0; y < sizes.Length; y++)
        {
            int size = sizes[y];

            for (int x = 0; x < size; x++)
            {
                for (int z = 0; z < size; z++)
                {
                    Vector3 offset = new Vector3(
                        x - size / 2,
                        y,
                        z - size / 2
                    );

                    Instantiate(leafPrefab, newSpawn + offset, Quaternion.identity, transform);
                }
            }
        }
    }

    bool CanPlaceTree(Vector3 spawnPos)
    {
        Vector2 newPos = new Vector2(spawnPos.x, spawnPos.z);

        foreach (Vector3 treePos in treePositions)
        {
            Vector2 oldPos = new Vector2(treePos.x, treePos.z);

            if (Vector2.Distance(newPos, oldPos) < treeDistance)
                return false;
        }

        return true;
    }
}