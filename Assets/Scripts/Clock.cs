using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;




public class Clock : MonoBehaviour
{
    [SerializeField] private Transform secondPointer;
    [SerializeField] private Transform minutePointer;
    [SerializeField] private Transform hourPointer;
    
    private float secondsDegrees;
    private float minuteDegrees;
    private float hourDegrees;
    private int timeInSeconds;
    private int previousSecond;



    private void Start()
    {
        secondsDegrees = 360f / 60f; //movement in degrees per second
        minuteDegrees = 360f / (60f * 60f); // 60seconds/min times 60minutes/hour 
        hourDegrees = 360f / (60f * 60f * 12f); // 60seconds/min times 60minutes/hour times amount of hour
    }

    void Update()
    {
        ConvertTimeToSeconds();
        RotateClockHands();
    }

    private int ConvertTimeToSeconds()
    {
        int currentSeconds = DateTime.Now.Second;
        int currentMinute = DateTime.Now.Minute;
        int currentHour = DateTime.Now.Hour;

        if (currentHour >= 12)
        {
            currentHour -= 12;
        }

        timeInSeconds = currentSeconds + (currentMinute * 60) + (currentHour * 60 * 60);
        return timeInSeconds;
    }

    private void RotateClockHands()
    {
        if (timeInSeconds != previousSecond) // update once per second instead of every frame
        {
            secondPointer.localRotation = Quaternion.Euler(90 + timeInSeconds * secondsDegrees,0,-90);
            minutePointer.localRotation = Quaternion.Euler(90+ timeInSeconds * minuteDegrees,0,-90); 
            hourPointer.localRotation = Quaternion.Euler(90+ timeInSeconds * hourDegrees,0,-90);
        }
        previousSecond = timeInSeconds;
    }
}
