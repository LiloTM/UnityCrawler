using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAI : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 direction;
    float min = -5f;
    float max = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        direction = randomDirection();
        rb.velocity = direction;//new Vector3(0,0,1);
        rb.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3.0f)) {
            //rb.velocity = Vector3.up;
            //rb.velocity = Quaternion.Euler(-90, 0, 0) * rb.velocity;
            //rb.MoveRotation(rb.rotation * Quaternion.Euler(rb.velocity * Time.fixedDeltaTime));
            
            //rb.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    Vector3 randomDirection(){
        var x = Random.Range(min, max);
        var z = Random.Range(min, max);

        Vector3 move = transform.right * x + transform.forward * z;
        return move;
    }
}
