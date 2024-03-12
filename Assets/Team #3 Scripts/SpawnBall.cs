using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    float timer = 0;
    public float resetTime = 5;

    public bool Check = false;
    //public Camera arCam;
    //public Transform CameraTransform;
    // bool check;
    // public float BallExpired = 5f;

    /* private void Start()
     {
         arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
         CameraTransform = arCam.transform;
     }*/
    private void Start()
    {
        ResetTimer();
    }

    public void Spawn()
    {
        Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
    }

    private void Update()
    {
        if (timer > 0 && Check == true)
        {
            timer -= Time.deltaTime;
            if (Check == true && timer <= 0)
            {
                Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
                Check = false;
                ResetTimer();
            }
        }

    }

    private void ResetTimer()
    {
        timer = resetTime;
    }
    /*public void ReAddBall()
    {
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        CameraTransform = arCam.transform;
        ball.SetActive(true);
    }

    private void Update()
    {
        check = ball.activeSelf;
        if (check == false)
        {
            float temp = 0.0f;
            for (int i = 0; i < BallExpired; i++)
            {
                temp += Time.deltaTime;

                if (temp >= BallExpired)
                {
                    ReAddBall();
                }
            }
        }
    }*/
}
