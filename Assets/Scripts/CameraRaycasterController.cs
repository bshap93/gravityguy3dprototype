using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycasterController : MonoBehaviour
{
    [SerializeField] private PhysicsRaycaster raycaster;
    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<PhysicsRaycaster>();

        // set the raycaster to ignore the UI layer
        raycaster.eventMask = ~LayerMask.GetMask("UI");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
