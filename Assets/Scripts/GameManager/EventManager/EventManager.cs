using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StringEvent : UnityEvent<string>
{
}

public class GameObjectEvent : UnityEvent<GameObject>
{
}

public class EventManager : MonoBehaviour
{
    // Instance
    public static EventManager Instance { get; private set; }

    [CanBeNull] public CapitalShipDockingBayCollider01 capitalShipDockingBayCollider01;
    public readonly StringEvent CommenceShipDocking = new StringEvent();

    void Start()
    {
        capitalShipDockingBayCollider01.onPlayerShipInDockingRange.AddListener(CommenceDocking);
    }

    void CommenceDocking()
    {
        CommenceShipDocking.Invoke("Press 'X' to dock");
    }


    public void StartFadeToBlack()
    {
        SceneManager.LoadScene("GameOverScene");
        // after 1 second, change to game over scene 
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameOverScene");
    }
}
