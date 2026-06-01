using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Getting the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotation around the X-axis (Looking up and down)
        xRotation -= mouseY;

        // Clamping the rotation so the player cannot look too far up or down
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // Rotation around the Y-axis (Looking left and right)
        yRotation += mouseX;

        // Applying the rotations to the transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}