using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPointController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.GetComponent<Rigidbody>().position;
        Quaternion rotation = new Quaternion(0, 
            player.GetComponent<Rigidbody>().rotation.y, 
            0, 
            player.GetComponent<Rigidbody>().rotation.w);
        gameObject.transform.rotation = rotation;
    }
    
    public void SetFocalPoint(GameObject focalPoint)
    {
        player = focalPoint;
    }
}
