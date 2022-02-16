using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 gravityVector;

    void Update()
    {      
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        /*gravityVector.y += gravity * Time.deltaTime;
        controller.Move(gravityVector * Time.deltaTime);*/

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * speed);
        
    }
}
