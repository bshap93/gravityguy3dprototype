using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAsteroidController : MonoBehaviour
{
    private Vector3 _initialRotationForceVector;
    private Vector3 _initialMovementForceVector;

    private GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("DebrisCluster_Prototype_01");
        _initialMovementForceVector = _parent.GetComponent<DebrisClusterTestController>().initialMovementForceVector;
        _initialRotationForceVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GetComponent<Rigidbody>().AddForce(_initialMovementForceVector, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(_initialRotationForceVector, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
