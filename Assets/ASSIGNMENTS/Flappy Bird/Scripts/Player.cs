using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public float velocity = 4f;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject gameOver;
    
    public bool isHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
        
        StartCoroutine("DelayGameEnd", 1f);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
            int noise = Random.Range(1, 4);
            FindFirstObjectByType<FlappyAudio>().Play("Jump" + noise);
            animator.SetTrigger("Flap");
        }
        HandleRotation();
    }
    private void HandleRotation()
    {
        if (rb.linearVelocity.y > 0)
        {
            
            transform.rotation = Quaternion.Euler(0f, 180f, 30f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, -30f);
        }
    }
    IEnumerator DelayGameEnd(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); 
        GameManager.Instance.GameOver();
    }
    
}
