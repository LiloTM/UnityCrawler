using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public GameObject FootPrefab;
    public GameObject LegPrefab;
    private GameObject Foot;
    private GameObject Leg;

    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            
            Foot = Instantiate(FootPrefab, hit.point, Quaternion.identity);
            
            /*Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);
            
            Vector3 newScale = Leg.transform.localScale;
            newScale.y = Vector3.Distance(transform.position, hit.point);
            Leg.transform.localScale = newScale;
            Leg.transform.LookAt(transform.position-new Vector3(0,0,90));*/

        }
        else{
            Foot = Instantiate(FootPrefab, transform.position, Quaternion.identity);
            //Leg = Instantiate(LegPrefab, transform.position, Quaternion.identity);
        }
        
    }

    void Update()
    {
        RaycastHit hit;
        //Vector3 newScale = Leg.transform.localScale;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            // if distance between foot and raycast downward bigger than value
            float distance = (hit.point - Foot.transform.position).magnitude;
            if(distance > 2){
                Destroy(Foot); 
                Foot = Instantiate(FootPrefab, hit.point, Quaternion.identity);
               
                /*Destroy(Leg); 
                Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);
                newScale.y = Vector3.Distance(transform.position, hit.point);
                Leg.transform.localScale = newScale;*/
                
            }
        }   
        //Leg.transform.LookAt(transform.position-new Vector3(0,0,90));
        
        
        
        
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        
        /*
        Vector3 pointA ... //one of the points
        Vector3 pointB ... //the other point
        float distanceBetween = (PointB - PointA).magnitude;
        //Then set cylinder size to that distance
        */        
    }
}
