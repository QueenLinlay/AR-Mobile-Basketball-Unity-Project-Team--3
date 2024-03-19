using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class AutoShoot : MonoBehaviour
{
    public float TimeStart = 0.0f;
    float TimeCountDown = 0.0f;
    public bool TimerDone = false;
    public bool Shooting = false;
    public GameObject RewardBall;
    public int Deleteball = 0;
    //public Transform ShootPosition;
    public float LaunchSpeed;
    public float UpwardsSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        TimerDone = true;
        TimeCountDown = TimeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerDone)
        {
            if (TimeCountDown > 0)
            {
                TimeCountDown -= Time.deltaTime;
                Shooting = false;
            }
            else
            {
                Debug.Log("Time ended, restarting");
                TimeCountDown = TimeStart;
                Shooting = true;
                ShootBall();
            }
        }
    }

    void ShootBall()
    {
        GameObject ball = Instantiate(RewardBall, transform.position, transform.rotation);
        //ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(UpwardsSpeed, LaunchSpeed, 0));
        ball.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * LaunchSpeed);
        Destroy(ball, Deleteball);
    }
}
