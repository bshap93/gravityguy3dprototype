using System;
using UnityEngine;
using UnityEngine.Events;


public class CapitalShipDockingBayCollider01 : MonoBehaviour
{
    public DockingBayCameraController dockingBayCameraController;
    public Animator dockingBayCameraAnimator;

    public UnityEvent onPlayerShipInDockingRange;

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
            onPlayerShipInDockingRange.Invoke();
        }
    }
}
