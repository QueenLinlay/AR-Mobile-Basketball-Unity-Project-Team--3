using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnModel : MonoBehaviour
{
    [SerializeField]
    private Vector3 position;
    private float width;
    private float height;
    public GameObject SpawnObject;
    public Camera arCam;
    public Transform CameraTransform;
    public float distance;
    public bool turnOff = false;
    SphereCollider ballCollider;
    Rigidbody BallRigid;
    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2 ;
        height = (float)Screen.height / 2;

        position = (CameraTransform.position + CameraTransform.forward * distance);
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        ballCollider = SpawnObject.GetComponent<SphereCollider>();
        BallRigid = SpawnObject.GetComponent<Rigidbody>();
        //ballCollider.enabled = false;
        //BallRigid.isKinematic = true;
        DisableBall(false, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Calling function, allowing ball to keep moving around until the screen is touch
        //Later will input if the user did not throw, the ball will return back to original 
        //position, then they are allow to throw the ball again
        UpdatePosition(turnOff);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Get the first touch since count touch
            turnOff = true;
           // ballCollider.enabled = false;
           // BallRigid.isKinematic = true;
            DisableBall(false, true);
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Statinoary ||  touch.phase == TouchPhase.Moved)
            {
                //Vector2 pos = touch.position;
                //pos.x = (pos.x - width) / width;
                //pos.y = (pos.y - height) / height;
                //position = new Vector3(pos.x / 2, pos.y / 2, position.z * distance);

                // position the spawn object
                //SpawnObject.transform.position = position;
                MovedObject(touch);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DisableBall(true, false);
               // ballCollider.enabled = true;
               // BallRigid.isKinematic = false;
            }
            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);
                
                if (touch.phase == TouchPhase.Began)
                {
                    // Halve the size of the cube
                    SpawnObject.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
                }
                
                if (touch.phase == TouchPhase.Ended)
                {
                    // Restore the regular size of the cube
                    SpawnObject.transform.localScale = new Vector3(5.077087f, 5.077087f, 5.077087f);
                }
            }

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
            SpawnObject.transform.position = position;
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
        Vector3 touchedPos = arCam.main.ScreenToWorldPoint(new Vector3(Temp.position.x,
            Temp.position.y, 10));
        //SpawnObject.transform.position = Vector3.lerp(SpawnObject.transform.position,
        // touchedPos, Time.deltaTime);
        //position = new Vector3(pos.x / 2, pos.y / 2, position.z * distance);
        SpawnObject.transform.position = Vector3.lerp(new Vector3(SpawnObject.transform.position.x,
            SpawnObject.transform.y, position.z * distance),
    touchedPos, Time.deltaTime);
    }
    
    //Disable ball of collider and rigid body
    void DisableBall(bool temp1, bool temp2)
    {
        ballCollider.enabled = temp1;
        BallRigid.isKinematic = temp2;
    }
}
