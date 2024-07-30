using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicAsteroidController : MonoBehaviour
{
    [FormerlySerializedAs("_initialRotationForceVector")]
    public Vector3 initialRotationForceVector;
    [FormerlySerializedAs("_initialMovementForceVector")]
    public Vector3 initialMovementForceVector;
    public float RotationIntensity = 100;

    public float hitPoints = 100f;
    [FormerlySerializedAs("_sphereColliderObject")] [FormerlySerializedAs("sphereCollider")]
    public SphereCollider sphereColliderObject;

    private GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("DebrisClusterStationary_01");
        initialMovementForceVector = Vector3.zero;
        initialRotationForceVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        // GetComponent<Rigidbody>().AddForce(_initialMovementForceVector * 1000, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(initialRotationForceVector * RotationIntensity, ForceMode.Impulse);


        CreateSphereCollider();
    }

    public void ReduceHitPoints(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            IncrementQuestVariable("Asteroids.NumDestroyed", 1);
        }
    }

    void IncrementQuestVariable(string variableName, int incrementAmount)
    {
        // Get the current value of the variable
        int currentValue = DialogueLua.GetVariable(variableName).asInt;
        Debug.Log($"Current value of {variableName}: {currentValue}");

        // Increment the value
        int newValue = currentValue + incrementAmount;

        // Set the new value
        DialogueLua.SetVariable(variableName, newValue);

        // Verify the new value
        int verifyValue = DialogueLua.GetVariable(variableName).asInt;
        Debug.Log($"New value of {variableName}: {verifyValue}");

        // Force update
        DialogueManager.Instance.SendUpdateTracker();
    }

    private void CreateSphereCollider()
    {
        sphereColliderObject = new SphereCollider();
        sphereColliderObject = gameObject.AddComponent<SphereCollider>();
        sphereColliderObject.radius = 100f;
        sphereColliderObject.isTrigger = true;
        sphereColliderObject.center = new Vector3(0, 0, 0);
    }
}
