using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float timeLeft;
    public bool timerOn = false;
    public TextMeshProUGUI timerText;
    public GameObject spawnManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void StartTimer()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTime(timeLeft);
            }
            else
            {
                timerText.text = "Time's up!";
                timeLeft = 0;
                timerOn = false;
            }
        }
    }
    
    public void UpdateTime(float currentTime)
    {
        currentTime += 1;
        
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void GameOver()
    {

    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
