using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MakeCube : MonoBehaviour
{
    private Vector3[] vertices =
    {
        new Vector3(0, 0, 0), // 0
        new Vector3(0, 1, 0), // 1
        new Vector3(0, 1, 1), // 2
        new Vector3(0, 0, 1), // 3
        new Vector3(1, 0, 0), // 4
        new Vector3(1, 1, 0), // 5
        new Vector3(1, 1, 1), // 6
        new Vector3(1, 0, 1), // 7
    };

    private int[] triangles =
    {
        // Left
        0, 2, 1,
        0, 3, 2,

        // Right
        4, 5, 6,
        4, 6, 7,

        // Back
        0, 1, 5,
        0, 5, 4,

        // Front
        3, 6, 2,
        3, 7, 6,

        // Top
        1, 2, 6,
        1, 6, 5,

        // Bottom
        0, 4, 7,
        0, 7, 3,
    };

    void Start()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }
}