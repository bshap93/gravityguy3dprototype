using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AsteroidInRangeController : MonoBehaviour
{
    public GameObject asteroid;
    public readonly GameObjectEvent AsteroidInRange = new();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AsteroidInRange.Invoke(asteroid);
        }
    }
}
