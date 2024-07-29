using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    public float panSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * panSpeed * Input.GetAxis("Horizontal")));
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * panSpeed * Input.GetAxis("Vertical")));
        }
    }
}
