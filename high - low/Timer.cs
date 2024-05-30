using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    public float countDownTime;
    public float timeRemaining;
    public bool timerIsRunning = false;

   
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Logger.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        //float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer.text = string.Format("{00}", seconds);
    }

    public void startTimer()
    {
        timerIsRunning = true;
        timeRemaining = countDownTime;
    }
}
