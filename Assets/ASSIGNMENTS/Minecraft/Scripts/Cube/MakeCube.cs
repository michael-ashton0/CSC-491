using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MakeCube : MonoBehaviour
{
    public enum blocks
    {
        GRASS_TOP,
        GRASS_SIDE,
        GRASS_BOTTOM,
        STONE,
        COAL,
        LOG,
        LEAF,
        IRON,
        DIAMOND
    }

    public blocks block_top;
    public blocks block_side;
    public blocks block_bottom;
    
    private Vector3[] vertices =
    {
        // Left face
        new Vector3(0, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 1, 1),
        new Vector3(0, 0, 1),

        // Right face
        new Vector3(1, 0, 1),
        new Vector3(1, 1, 1),
        new Vector3(1, 1, 0),
        new Vector3(1, 0, 0),

        // Back face
        new Vector3(1, 0, 0),
        new Vector3(1, 1, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 0),

        // Front face
        new Vector3(0, 0, 1),
        new Vector3(0, 1, 1),
        new Vector3(1, 1, 1),
        new Vector3(1, 0, 1),

        // Top face
        new Vector3(0, 1, 0),
        new Vector3(0, 1, 1),
        new Vector3(1, 1, 1),
        new Vector3(1, 1, 0),

        // Bottom face
        new Vector3(0, 0, 1),
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(1, 0, 1),
    };

    private int[] triangles =
    {
        // Left
        2, 1, 0,
        3, 2, 0,
        
        // Right
        6, 5, 4,
        7, 6, 4,

        // Back
        10, 9, 8,
        11, 10, 8,

        // Front
        14, 13, 12,
        15, 14, 12,

        // Top
        16, 17, 18,
        16, 18, 19,

        // Bottom
        20, 21, 22,
        20, 22, 23,
    };

    static Vector2[] GetIndices(int x, int y)
    {
        Vector2[] indices = new Vector2[4];

        const float WIDTH = 16f;
        const float HEIGHT = 16f;

        indices[0] = new Vector2(x / WIDTH, y / HEIGHT);
        indices[1] = indices[0] + new Vector2(0f, 1f / HEIGHT);
        indices[2] = indices[0] + new Vector2(1f / WIDTH, 1f / HEIGHT);
        indices[3] = indices[0] + new Vector2(1f / WIDTH, 0f);

        return indices;
    }

    static Vector2[] GetIndicesByBlock(blocks blockType)
    {
        switch (blockType)
        {
            case blocks.GRASS_TOP:
                return GetIndices(0, 15);

            case blocks.GRASS_SIDE:
                return GetIndices(3, 15);

            case blocks.GRASS_BOTTOM:
                return GetIndices(2, 15);

            case blocks.STONE:
                return GetIndices(0, 14);
  
            case blocks.COAL:
                return GetIndices(2, 13);
            
            case blocks.LOG:
                return GetIndices(4, 14);
            
            case blocks.LEAF:
                return GetIndices(4, 12);
            
            case blocks.IRON:
                return GetIndices(1, 13);
            
            case blocks.DIAMOND:
                return GetIndices(2, 12);
            
            default:
                return GetIndices(0, 0);
        }
    }

    void Start()
    {
        List<Vector2> uvs = new List<Vector2>();

        Vector2[] topUVs = GetIndicesByBlock(block_top);
        Vector2[] sideUVs = GetIndicesByBlock(block_side);
        Vector2[] bottomUVs = GetIndicesByBlock(block_bottom);

        uvs.AddRange(sideUVs);
        uvs.AddRange(sideUVs);
        uvs.AddRange(sideUVs);
        uvs.AddRange(sideUVs);
        uvs.AddRange(topUVs);
        uvs.AddRange(bottomUVs);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }
}