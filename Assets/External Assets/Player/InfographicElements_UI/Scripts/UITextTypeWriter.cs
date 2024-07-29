using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextTypeWriter : MonoBehaviour
{
    public EventManager eventManager;
    Text txt;
    string story;

    void Start()
    {
        eventManager.CommenceShipDocking.AddListener(ChangeText);
    }

    void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
    }

    public void ChangeText(string text)
    {
        story = text;


        StartCoroutine("PlayText");
    }
    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }
}
