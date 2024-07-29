using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    void Start()
    {
        
        

    }
    // Update is called once per frame
    void Update()
    {

    }
    
    public void SpwanDebrisCluster(Vector3 spawnLocation, int clusterSize)
    {
        for (int i = 0; i < clusterSize; i++)
        {
            Instantiate(Resources.Load("Debris"), spawnLocation, Quaternion.identity);
        }
    }
    
    


}
