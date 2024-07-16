using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private float verticalRotation = 0.0f;

    void Update()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(horizontal, 0, vertical);

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(0, mouseX, 0);
    }
}
