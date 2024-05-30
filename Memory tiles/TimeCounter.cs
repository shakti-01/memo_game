using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCounter;
    private bool timerIsRunning;
    private float timeValue;

    public void StartTimer()
    {
        timerIsRunning = true;
        timeValue = 0;
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            timeValue += Time.deltaTime;
            DisplayTime(timeValue);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public string GetTimeValue()
    {
        float time = timeValue;
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
