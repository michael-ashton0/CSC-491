using UnityEngine;

public class MakeCube : MonoBehaviour
{
    private Vector3[] vertices =
        {
        new Vector3(0, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 1, 1),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 0),
        new Vector3(1, 1, 0),
        new Vector3(1, 1, 1),
        new Vector3(1, 0, 1),
    };

    public int[] triangles =
    {
        //Front
        0, 1, 5,
        1, 5, 4,
        //Left
        4, 2, 0,
        2, 1, 0,
        //Back
        7, 1, 3,
        1, 6, 7,
        //Right
        5, 6, 7,
        5, 4, 7,
        //Top
        1, 2, 4,
        2, 6, 4,
        //Bottom
        0, 3, 4,
        3, 7, 4,
    };
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        filter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        // Mesh mesh = new Mesh();
        // mesh.vertices = vertices;
        // mesh.triangles = triangles;
        //
        // MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        // filter.mesh = mesh;
    }
}
