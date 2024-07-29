using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieDoorController : MonoBehaviour
{
    public List<GameObject> doorParts;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OpenDoor()
    {
        foreach (GameObject doorPart in doorParts)
        {
            doorPart.GetComponent<Animator>().SetTrigger("isOpen");
        }
    }
}
