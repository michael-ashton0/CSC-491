using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public float mouseSensitivity = 200f;

    public Camera cam;
    public Transform playerBody;

    private CharacterController controller;
    private float yVelocity;
    private float xRotation = 0f;
    private float yRotation = 0f;
    
    public float maxDistance = 10f;
    public GameObject cubePrefab;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cam == null)
            cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleAiming();
        HandleMovement();
        if (Input.GetMouseButtonDown(0))
        {
            TryDestroyBlock();
        }

        if (Input.GetMouseButtonDown(1))
        {
            TryPlaceBlock();
        }
    }
    
    Ray GetCenterRay()
    {
        return cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
    }
    
    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z).normalized;
        Vector3 worldMove = playerBody.forward * move.z + playerBody.right * move.x;

        if (controller.isGrounded && yVelocity < 0f)
            yVelocity = -2f;

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = worldMove * moveSpeed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }
    void HandleAiming()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        yRotation -= mouseX;
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    
    void TryDestroyBlock()
    {
        Ray ray = GetCenterRay();

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) & gameObject.CompareTag("Cube"))
        {
            Destroy(hit.collider.gameObject);
        }
    }

    void TryPlaceBlock()
    {
        Ray ray = GetCenterRay();

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance) & gameObject.CompareTag("Cube"))
        {
            Vector3 placePos = hit.collider.transform.position + hit.normal;

            Instantiate(cubePrefab, placePos, Quaternion.identity);
        }
    }
}

