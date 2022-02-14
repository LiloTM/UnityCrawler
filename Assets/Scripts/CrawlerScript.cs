using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerScript : MonoBehaviour
{
    public GameObject FootPrefab;
    private GameObject Foot;
    float threshold;
    // Start is called before the first frame update
    void Start()
    {
        threshold = Random.Range(1.0f, 2.5f);
        Debug.Log("Threshold: " + threshold);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            Foot = Instantiate(FootPrefab, hit.point, Quaternion.identity);
        }
        else{
            Foot = Instantiate(FootPrefab, transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
        {
            // if distance between foot and raycast downward bigger than value
            float distance = (hit.point - Foot.transform.position).magnitude;
            if(distance > threshold){
                Destroy(Foot); 
                Foot = Instantiate(FootPrefab, hit.point, Quaternion.identity);
                threshold = Random.Range(1.0f, 2.5f);
                Debug.Log("Threshold: " + threshold);
            }
        }
    }
}