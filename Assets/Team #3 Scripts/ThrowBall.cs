using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// Reference:
// https://www.youtube.com/watch?v=7O9bAFyGvH8
// https://discussions.unity.com/t/unity-touch-controls-accurate-finger-following/108726/2
// https://discussions.unity.com/t/how-to-make-an-object-follow-the-camera-orientation/201498/2

public class ThrowBall : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [SerializeField]
    float throwForceInXandY = 1;

    [SerializeField]
    float throwForceInZ = 50f;

    Rigidbody rb;

    [SerializeField]
    private Vector3 position;
    //public GameObject SpawnObject;
    public Camera arCam;
    public Transform CameraTransform;
    public float distance;
    public bool turnOff = false;

    [SerializeField]
    public float Speed = 0;
    public float BallExpired = 5f;

    SphereCollider ballCollider;
    //public TrailRenderer Trail;

    // Start is called before the first frame update
    void Start()
    {
        ballCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        CameraTransform = arCam.transform; 
        position = (CameraTransform.position + CameraTransform.forward * distance);
       // Trail = GetComponent<TrailRenderer>();
        DisableBall(false, true);
    }

    // Update is called once per frame
    void Update()
    {

        UpdatePosition(turnOff);
        //Trail.enabled = false;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            Touch touch = Input.GetTouch(0);
            turnOff = true;
            DisableBall(false, true);
           // Trail.enabled = true;
            // Geting touch position and working time when you touch the screen
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended) 
            {
                MoveObject(touch);
            }

        }

        //if you release your finger)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            touchTimeFinish = Time.time;

            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.GetTouch(0).position;

            direction = startPos - endPos;

            DisableBall(true, false);
            rb.AddForce(-direction.x * throwForceInXandY, -direction.y *
                throwForceInXandY, throwForceInZ / timeInterval);

            Destroy(gameObject, BallExpired);
        }
    }

    //Update position of the ball, allowing the ball to move around base on position of camera.
    void UpdatePosition(bool TurnOff)
    {
        int check = 0;
        if (TurnOff == false)
        {

            // Update position of ball if camera is moving
            position = (CameraTransform.position + CameraTransform.forward * distance);
            transform.position = position;
        }
        else if (turnOff == true & check == 0)
        {

            Debug.Log("No longer connected to screen!!");
            check++;
        }
    }

    // Code that allow moving the main object in front of the camera.
    void MoveObject(Touch Temp)
    {
        Debug.Log(position.z);
        Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(Temp.position.x,
            Temp.position.y, GetCameraZPosition()));

        transform.position = Vector3.Lerp(transform.position,
         touchedPos, Time.deltaTime * Speed);
    }

    //Disable ball of collider and rigid body
    void DisableBall(bool temp1, bool temp2)
    {
        ballCollider.enabled = temp1;
        rb.isKinematic = temp2;
    }

    //Get the new position of Camera, to make sure the Z position is base on the Camera Position. 
    float GetCameraZPosition()
    {
        float Temp;
        position = (CameraTransform.position + CameraTransform.forward);
        // Change to - of position for the other way, if positive, it wont work but go backwards of the camera.
        Temp = -position.z;
        return Temp;
    }
}
