using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCam;
    public Camera thirdPersonCam;
    public Camera firstPersonCam;

    public float mouseSensitivity = 150f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        mainCam.enabled = true;
        thirdPersonCam.enabled = false; 
        firstPersonCam.enabled = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            mainCam.enabled = true;
            thirdPersonCam.enabled = false; 
            firstPersonCam.enabled = false; 
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            mainCam.enabled = false;
            thirdPersonCam.enabled = true; 
            firstPersonCam.enabled = false; 
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            mainCam.enabled = false;
            thirdPersonCam.enabled = false; 
            firstPersonCam.enabled = true; 
        }

        if( thirdPersonCam.enabled == true){
            Cursor.lockState = CursorLockMode.None;
        }
        if(mainCam.enabled == true || firstPersonCam.enabled == true){
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
