using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 40f;
    public float ySensitivity = 45f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // calculate camera rotation up down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        //transform camera
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //look left right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
