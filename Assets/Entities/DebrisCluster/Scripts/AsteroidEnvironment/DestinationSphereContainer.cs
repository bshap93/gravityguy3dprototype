using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestinationSphereContainer : MonoBehaviour
{
    public TextMeshProUGUI numText;
    private List<GameObject> _debrisList = new List<GameObject>();
    private SphereCollider _sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        PollForNumDebris();
    }

    private void PollForNumDebris()
    {
        _debrisList.Clear();
        _sphereCollider = GetComponent<SphereCollider>();
        // find number or debris in the scene
        GameObject[] debris = GameObject.FindGameObjectsWithTag("Debris");
        foreach (GameObject d in debris)
        {
            if (_sphereCollider.bounds.Contains(d.transform.position))
                _debrisList.Add(d);
        }
        numText.text = _debrisList.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        PollForNumDebris();
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            _debrisList.Remove(other.gameObject);
            PollForNumDebris();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Debris"))
        {
            _debrisList.Add(other.gameObject);
            PollForNumDebris();
        }
    }
}
