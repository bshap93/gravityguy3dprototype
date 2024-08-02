using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    public static LaserPool Instance;
    public GameObject laserPrefab;
    public int poolSize = 20;

    private List<GameObject> pool;

    void Awake()
    {
        Instance = this;
        InitializePool();
    }

    void InitializePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.SetActive(false);
            pool.Add(laser);
        }
    }

    public GameObject GetLaser()
    {
        foreach (GameObject laser in pool)
        {
            if (!laser.activeInHierarchy)
            {
                laser.SetActive(true);
                return laser;
            }
        }

        // If all lasers are in use, create a new one
        GameObject newLaser = Instantiate(laserPrefab);
        pool.Add(newLaser);
        return newLaser;
    }

    public void ReturnLaser(GameObject laser)
    {
        laser.SetActive(false);
    }
}
