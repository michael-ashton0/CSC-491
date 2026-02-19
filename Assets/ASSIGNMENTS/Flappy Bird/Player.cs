using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity = 4f;
    private Rigidbody2D rb;

    public GameObject gameOver;
    
    public bool isHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D collider = gameObject.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
        }
    }
    
}
