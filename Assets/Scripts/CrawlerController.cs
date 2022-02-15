using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : MonoBehaviour
{
    public GameObject FootPrefab;
    public GameObject LegPrefab;
    private GameObject Foot;
    private GameObject Leg;
    float threshold;
    Vector3 newLegScale;
    RaycastHit hit;
    
    void Start()
    {
        threshold = Random.Range(1.0f, 2.0f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity)) {
            Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);  
            LegUpdate();
        }
        else {
            Foot = Instantiate(FootPrefab, transform.position, Quaternion.identity);
            Leg = Instantiate(LegPrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        LegUpdate();
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity)) {
            // updates if the distance between foot and box bigger than value
            float distance = (hit.point - Foot.transform.position).magnitude;
            if(distance > threshold){
                // creates a new foot
                Destroy(Foot); 
                Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                threshold = Random.Range(1.0f, 2.0f);

                // creates a new leg
                Destroy(Leg); 
                Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);
                LegUpdate();
            }
        }
    }

    void LegUpdate()
    {   //updates length and rotation of the legs 
        newLegScale = Leg.transform.localScale;
        newLegScale.z = Vector3.Distance(transform.position, Foot.transform.position);
        Leg.transform.localScale = newLegScale;
        Leg.transform.LookAt(transform.position);
    }
}