using UnityEngine;
using System.Collections.Generic;
using System;
public class BlockStorage : MonoBehaviour
{
    Dictionary<(float, float, float), int> blockDict = new Dictionary<(float, float, float), int>();
    public Tuple<int, int> viewBox = new Tuple<int, int>(4, 4);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void addBlock(Vector3 blockPos, int blockType)
    {
        blockDict.Add((blockPos.x, blockPos.y, blockPos.z), blockType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
