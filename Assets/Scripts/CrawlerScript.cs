using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerScript : MonoBehaviour
{
    public GameObject FootPrefab;
    public GameObject LegPrefab;
    private GameObject Foot;
    private GameObject Leg;
    float threshold;

    void Start()
    {
        threshold = Random.Range(1.0f, 2.0f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);
            
            Vector3 newScale = Leg.transform.localScale;
            newScale.z = Vector3.Distance(transform.position, hit.point);
            Leg.transform.localScale = newScale;
            Leg.transform.LookAt(transform.position);
        }
        else{
            Foot = Instantiate(FootPrefab, transform.position, Quaternion.identity);

            Leg = Instantiate(LegPrefab, transform.position, Quaternion.identity);
        }
        
    }

    void Update()
    {
        Vector3 newScale = Leg.transform.localScale;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity)/* && !hit.collider.gameObject.Leg && !hit.collider.gameObject.Foot*/)
        {
            // if distance between foot and raycast downward bigger than value
            float distance = (hit.point - Foot.transform.position).magnitude;
            if(distance > threshold){
                Destroy(Foot); 
                Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                threshold = Random.Range(1.0f, 2.0f);

                Destroy(Leg); 
                Leg = Instantiate(LegPrefab, hit.point, Quaternion.identity);
                newScale.z = Vector3.Distance(transform.position, Foot.transform.position);
                Leg.transform.localScale = newScale;
            }
        }

        newScale.z = Vector3.Distance(transform.position, Foot.transform.position);
        Leg.transform.localScale = newScale;
        Leg.transform.LookAt(transform.position);

    }
    // TODO: Legs make me cry
}