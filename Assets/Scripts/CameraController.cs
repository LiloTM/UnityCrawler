using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    public float mouseSensitivity = 150f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
            
        }
        if(cam1.enabled == true){
            Cursor.lockState = CursorLockMode.None;
        }
        if(cam2.enabled == true){
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
