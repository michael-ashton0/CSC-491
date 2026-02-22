using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.01f;

    private Material mat;
    private Vector2 offset;

    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        offset.x -= scrollSpeed * Time.deltaTime;

        mat.SetTextureOffset("_MainTex", offset);
    }
}