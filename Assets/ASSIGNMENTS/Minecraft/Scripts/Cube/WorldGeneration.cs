using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldGeneration : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject stonePrefab;
    public GameObject dirtPrefab;
    public GameObject logPrefab;
    public GameObject leafPrefab;
    
    public GameObject coalPrefab;
    public GameObject ironPrefab;
    public GameObject diamondPrefab;

    public float oreChance = 1f;
    public float coalChance = 0.33f;
    public float ironChance = 0.33f;
    public float diamondChance = 0.33f;
    
    public int width = 5;
    public int height = 5;
    public float spacing = 1f;

    public float treeChance;
    public float treeDistance;
    
    public int terrain_height_scaler = 3;
    public int tree_height_scaler = 5;

    private List<Vector3> treePositions;
    private Dictionary<Vector3, GameObject> stoneBlocks;
    
    void Start()
    {
        treePositions = new List<Vector3>();
        stoneBlocks = new Dictionary<Vector3, GameObject>();
        
        SpawnTopLayer();

        float oreRoll = Random.Range(0f, 1f);

        if (oreRoll <= oreChance)
        {
            float typeOfOreRoll = Random.Range(0f, 1f);
            SpawnOre(typeOfOreRoll);
        }
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

            GameObject stone = Instantiate(stonePrefab, newSpawn, Quaternion.identity, transform);
            stoneBlocks[newSpawn] = stone;
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
    
    void SpawnOreVein(Vector3 startPos, int veinSize)
    {
        Vector3 current = startPos;

        for (int i = 0; i < veinSize; i++)
        {
            if (stoneBlocks.ContainsKey(current))
            {
                Destroy(stoneBlocks[current]);

                Instantiate(coalPrefab, current, Quaternion.identity, transform);

                stoneBlocks.Remove(current);
            }
            
            int dir = Random.Range(0, 6);

            switch (dir)
            {
                case 0: current += Vector3.right; break;
                case 1: current += Vector3.left; break;
                case 2: current += Vector3.forward; break;
                case 3: current += Vector3.back; break;
                case 4: current += Vector3.up; break;
                case 5: current += Vector3.down; break;
            }
        }
    }
    
    void SpawnOre(float roll)
    {
        GameObject oreType;
        int veinSize;

        if (roll < coalChance)
        {
            oreType = coalPrefab;
            veinSize = Random.Range(8, 12);
        }
        else if (roll < coalChance + ironChance)
        {
            oreType = ironPrefab;
            veinSize = Random.Range(6, 8);
        }
        else
        {
            oreType = diamondPrefab;
            veinSize = Random.Range(4, 6);
        }
        
        int numVeins = 10;
        
        List<Vector3> stonePositions = new List<Vector3>(stoneBlocks.Keys);

        for (int i = 0; i < numVeins; i++)
        {
            if (stonePositions.Count == 0)
            {
                return;
            }

            Vector3 start = stonePositions[Random.Range(0, stonePositions.Count)];

            SpawnOreVein(start, veinSize);
        }
    }
    
}