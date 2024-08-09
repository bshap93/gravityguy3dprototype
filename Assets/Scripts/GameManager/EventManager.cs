using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class StringEvent : UnityEvent<string>
{
}

public class GameObjectEvent : UnityEvent<GameObject>
{
}

public class EventManager : MonoBehaviour
{
    [CanBeNull] public CapitalShipDockingBayCollider01 capitalShipDockingBayCollider01;
    public List<SphereCollider> debrisColldiers;
    public readonly StringEvent CommenceShipDocking = new StringEvent();
    public readonly GameObjectEvent AsteroidInRange = new GameObjectEvent();
    void Start()
    {
        capitalShipDockingBayCollider01.onPlayerShipInDockingRange.AddListener(CommenceDocking);

        var asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject asteroid in asteroids)
        {
            asteroid.GetComponent<AsteroidInRangeController>().TargetInRange
                .AddListener(SignalToShipThatAsteroidIsInRange);

            asteroid.GetComponent<AsteroidInRangeController>().TargetOutOfRange
                .AddListener(SignalToShipThatAsteroidIsInRange);

            debrisColldiers.Add(asteroid.GetComponent<SphereCollider>());
        }
    }

    void CommenceDocking()
    {
        CommenceShipDocking.Invoke("Press 'X' to dock");
    }

    void SignalToShipThatAsteroidIsInRange(GameObject asteroid)
    {
        Debug.Log("Asteroid is in range");
        AsteroidInRange.Invoke(asteroid);
    }
}
