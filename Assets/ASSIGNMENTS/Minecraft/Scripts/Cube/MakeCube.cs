using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MakeCube : MonoBehaviour
{
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
    
    static Vector2[] GetIndices(Vector2 topLeft)
    {
        return new Vector2[]
        {
            new Vector2(topLeft.x, topLeft.y - (1/16f)),
            new Vector2(topLeft.x, topLeft.y),
            new Vector2(topLeft.x + (1/16f), topLeft.y),
            new Vector2(topLeft.x + (1/16f), topLeft.y - (1/16f)),
        };
    }

    void Start()
    {
        List<Vector2> uvs = new List<Vector2>();
        
        Vector2[] sideUVs = GetIndices(new Vector2((3/16f), 1f));
        Vector2[] topUVs  = GetIndices(new Vector2(0f, 1f));
        Vector2[] bottomUVs = GetIndices(new Vector2((2/16f), 1f));
        
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