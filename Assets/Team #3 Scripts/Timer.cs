using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class Timer : MonoBehaviour
{
    public float TimeRemaining = 60;
    public bool TimerOn = false;

    public TextMeshProUGUI Timer_Text; 
    void Start()
    {
        TimerOn = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn)
        {
            if(TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime; 
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeRemaining = 0;
                TimerOn = false;
                UnityEngine.Application.Quit();
                Application.Quit();
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float mintues = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        Timer_Text.text = string.Format("{0:00} : {1:00}", mintues, seconds); 
    }
}
