using UnityEngine;

public class MazeTilt : MonoBehaviour
{
    public float maxTilt = 15f;
    public float tiltSpeed = 8f;

    float currentX;
    float currentZ;

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float targetZ = -inputX * maxTilt; // weird to think about these but I guess makes sense
        float targetX =  inputY * maxTilt;
        
        //chat helped with these when asked how to make the game feel better
        currentX = Mathf.Lerp(currentX, targetX, tiltSpeed * Time.deltaTime);
        currentZ = Mathf.Lerp(currentZ, targetZ, tiltSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentX, 0f, currentZ);
    }
}