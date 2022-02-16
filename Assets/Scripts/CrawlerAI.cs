using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAI : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 direction;
    float min = -1f;
    float max = 1f;

    float moveSpeed = 6f; 
    float turnSpeed = 90f; // degrees/second
    float lerpSpeed = 10f; // smoothing speed
    float gravity = 9.81f; // gravity acceleration
    
    private Vector3 surfaceNormal; // normal of current surface
    private Vector3 myNormal;   // normal of the character
    private float distanceGround; 
    private bool jumping = false; 

    float x;
    float z;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        myNormal = transform.up; // normal starts as character up direction 
        rb.freezeRotation = true; // disable physics rotation
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity)) {
            distanceGround = transform.position.y - hit.point.y;  // distance from transform.position to ground
        }

        x = Random.Range(min, max);
        z = Random.Range(min, max);

        direction = new Vector3(0,0,1);//randomDirection();
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);
    }

    void FixedUpdate(){       
        rb.AddForce(-gravity*rb.mass*myNormal); // apply constant weight force according to character normal
    }

    void Update()
    {
        ////// jump to wall code //////
        if (jumping) return;  // abort Update while jumping to a wall
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3)){ // wall ahead?
            JumpToWall(hit.point, hit.normal); // yes: jump to the wall
        }  
        rb.velocity = direction;
        rb.rotation = Quaternion.LookRotation(direction);      
        return;    
        ////// movement //////
        transform.Rotate(0, x * turnSpeed * Time.deltaTime, 0);
        
        if (Physics.Raycast(transform.position, -myNormal, out hit)) surfaceNormal = hit.normal; // update surface normal
        else surfaceNormal = Vector3.up;     // usual ground normal is Vector.up
        
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
        var myForward = Vector3.Cross(transform.right, myNormal);       // find forward direction with new myNormal

        // align character to the new myNormal while keeping the forward direction:
        var targetRot = Quaternion.LookRotation(myForward, myNormal);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);

        // move the character forth/back with Vertical axis:
        transform.Translate(0, 0, z * moveSpeed * Time.deltaTime);

        rb.rotation = Quaternion.LookRotation(direction);
    }

    void JumpToWall(Vector3 point, Vector3 normal){
        // jump to wall 
        jumping = true; 
        rb.isKinematic = true; 

        var orgPos = transform.position;
        var orgRot = transform.rotation;
        var dstPos = point + normal * (distanceGround); // will jump to 0.5 above wall

        var myForward = Vector3.Cross(transform.right, normal);
        var dstRot = Quaternion.LookRotation(myForward, normal);

        for (float t = 0.0f; t < 1.0f; ){
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(orgPos, dstPos, t);
            transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
        }

        myNormal = normal; 
        rb.isKinematic = false; 
        jumping = false; 
    }

    Vector3 randomDirection(){
        var x = Random.Range(min, max);
        var z = Random.Range(min, max);

        Vector3 move = transform.right * x + transform.forward * z;
        return move;
    }
}
