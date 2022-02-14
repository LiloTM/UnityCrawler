using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerScript : MonoBehaviour
{
    public GameObject FootPrefab;
    private GameObject Foot;
    float threshold;

    void Start()
    {
        threshold = Random.Range(1.0f, 2.5f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else{
            Foot = Instantiate(FootPrefab, transform.position, Quaternion.identity);
        }
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            // if distance between foot and raycast downward bigger than value
            float distance = (hit.point - Foot.transform.position).magnitude;
            if(distance > threshold){
                Destroy(Foot); 
                Foot = Instantiate(FootPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                threshold = Random.Range(1.0f, 2.5f);
            }
        }
    }
    // TODO: Legs make me cry
}