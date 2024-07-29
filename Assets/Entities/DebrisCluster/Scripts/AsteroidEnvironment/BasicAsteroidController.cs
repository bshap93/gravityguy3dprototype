using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicAsteroidController : MonoBehaviour
{
    private Vector3 _initialRotationForceVector;
    private Vector3 _initialMovementForceVector;

    public float hitPoints = 100f;
    [FormerlySerializedAs("sphereCollider")]
    public GameObject sphereColliderObject;

    private GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("DebrisCluster_Prototype_01");
        _initialMovementForceVector = _parent.GetComponent<DebrisClusterTestController>().initialMovementForceVector;
        _initialRotationForceVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GetComponent<Rigidbody>().AddForce(_initialMovementForceVector, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(_initialRotationForceVector, ForceMode.Impulse);


        CreateSphereCollider();
    }

    private void CreateSphereCollider()
    {
        sphereColliderObject.transform.parent = transform;
        sphereColliderObject.transform.localPosition = Vector3.zero;
        sphereColliderObject.AddComponent<SphereCollider>();
        sphereColliderObject.GetComponent<SphereCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
