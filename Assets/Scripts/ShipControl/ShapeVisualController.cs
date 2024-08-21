using System;
using UnityEngine;

namespace ShipControl
{
    public class ShapeVisualController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
        }
    }
}
