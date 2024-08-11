using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpTextController : MonoBehaviour
{
    public Text helpText;
    void Start()
    {
        helpText.text = "Seeking signal...";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
