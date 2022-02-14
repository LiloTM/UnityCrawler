using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerScript : MonoBehaviour
{
    public GameObject CrawlerFoot;
    private GameObject Foot1;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            Foot1 = Instantiate(CrawlerFoot, hit.point, Quaternion.identity);
        }
        else{
            Foot1 = Instantiate(CrawlerFoot, transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            // if distance between foot and raycast downward bigger than value
            float distance = (hit.point - Foot1.transform.position).magnitude;
            if(distance > 2){
                Destroy(Foot1); 
                Foot1 = Instantiate(CrawlerFoot, hit.point, Quaternion.identity);
            }
            
            //Debug.Log("Did Hit");
            
            
            /*
            Vector3 pointA ... //one of the points
            Vector3 pointB ... //the other point
            float distanceBetween = (PointB - PointA).magnitude;
            //Then set cylinder size to that distance
            */

            /*GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = new Vector3(-2, 1, 0);*/
        }
    }
}
