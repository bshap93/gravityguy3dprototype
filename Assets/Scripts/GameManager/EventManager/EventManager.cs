using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StringEvent : UnityEvent<string>
{
}

public class GameObjectEvent : UnityEvent<GameObject>
{
}

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    [FormerlySerializedAs("DeathEvent")] public UnityEvent<string> deathEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize your events here
        deathEvent = new UnityEvent<string>();
    }


    public void PlayerDeath()
    {
        deathEvent = new StringEvent();
        deathEvent.Invoke("Player has died");
    }


    public void StartFadeToBlack()
    {
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
