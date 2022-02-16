using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// credit: https://answers.unity.com/questions/155907/basic-movement-walking-on-walls.html

public class WallMovement : MonoBehaviour
{
    public Rigidbody rb;

    float moveSpeed = 6f; // move speed
    float turnSpeed = 90f; // turning speed (degrees/second)
    float lerpSpeed = 10f; // smoothing speed
    float gravity = 9.81f; // gravity acceleration
    bool isGrounded;
    float deltaGround = 0.2f; // character is grounded up to this distance
    float jumpSpeed = 10f; // vertical jump initial speed
    
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    private float vertSpeed = 0; // vertical jump current speed 
    
    void Start(){
        myNormal = transform.up; // normal starts as character up direction 
        rb.freezeRotation = true; // disable physics rotation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity)) {
            distGround = transform.position.y - hit.point.y;  // distance from transform.position to ground
        }
        
    }
    
    void FixedUpdate(){
        // apply constant weight force according to character normal:
        rb.AddForce(-gravity*rb.mass*myNormal);

    }
    
    void Update(){
        // jump code - jump to wall or simple jump
        if (jumping) return;  // abort Update while jumping to a wall
        
        RaycastHit hit;
        if (Input.GetButtonDown("Jump")){ // jump pressed:
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5)){ // wall ahead?
                JumpToWall(hit.point, hit.normal); // yes: jump to the wall
            }        
        }
        
        // movement code - turn left/right with Horizontal axis:
        transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);

        // update surface normal and isGrounded:
        if (Physics.Raycast(transform.position, -myNormal, out hit)){ // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up; 
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);

        // find forward direction with new myNormal:
        var myForward = Vector3.Cross(transform.right, myNormal);

        // align character to the new myNormal while keeping the forward direction:
        var targetRot = Quaternion.LookRotation(myForward, myNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);

        // move the character forth/back with Vertical axis:
        transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); 
    }
    
    void JumpToWall(Vector3 point, Vector3 normal){
        // jump to wall 
        jumping = true; // signal it's jumping to wall
        //rb.isKinematic = true; // disable physics while jumping

        var orgPos = transform.position;
        var orgRot = transform.rotation;
        var dstPos = point + normal * (distGround); // will jump to 0.5 above wall

        var myForward = Vector3.Cross(transform.right, normal);
        var dstRot = Quaternion.LookRotation(myForward, normal);
        for (float t = 0.0f; t < 1.0f; ){
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(orgPos, dstPos, t);
            transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            //yield return null;; // return here next frame
        }
        myNormal = normal; // update myNormal
        //rb.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished
    }
}