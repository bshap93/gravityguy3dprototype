using System.Collections;
using System.Collections.Generic;
using PixelCrushers.QuestMachine;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestNodeHUDItem : MonoBehaviour
{
    [SerializeField] public ProgressBar ProgressBar;
    [SerializeField] float currentPercent;
    [SerializeField] float maxValue;
    [SerializeField] float minValue;
    [SerializeField] QuestNode questNode;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
