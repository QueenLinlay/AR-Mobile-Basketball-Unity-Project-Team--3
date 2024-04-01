using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    // Reference: https://www.youtube.com/watch?v=u_n3NEi223E

    public float TimeRemaining = 60;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerNumber; 
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
                SceneManager.LoadScene(sceneBuildIndex:0);
                //UnityEngine.Application.Quit();
                //Application.Quit();
            }
            updateTimer();
        }
    }

    void updateTimer()
    {
        TimerNumber.text = TimeRemaining.ToString("0"); 
    }
}
