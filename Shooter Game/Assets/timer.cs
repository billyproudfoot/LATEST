using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class timer : MonoBehaviour
{
    public static timer instance;
    public Text timeCounter;
    private TimeSpan timePlaying;
    private bool timerGoing;
    public float totalTime;
        
    void Start()
    {
        instance = this;
        timeCounter.text = "Time: 00:00.00";
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        totalTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            totalTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(totalTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

    public string getTime()
    {
        return timePlaying.ToString("mm':'ss'.'ff");
    }

    void Update()
    {
        
    }
}