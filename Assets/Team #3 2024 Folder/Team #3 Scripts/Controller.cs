using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float jumpSpeed;

    public float ySpeed;
    Rigidbody rBody;

    public bool jumpIsReady;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

        transform.position = new Vector3(2.23861051f, -1.62899995f, 1.52878571f);
    }

    void Update()
    {
        //ySpeed += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpIsReady)
            {
                ySpeed = jumpSpeed;
                rBody.AddForce(Vector3.up * ySpeed, ForceMode.Impulse);
                jumpIsReady = false;
            }

        }
        // rBody.velocity += new Vector3(0.0f, ySpeed, 0.0f);
        //rBody.AddForce(Vector3.up * ySpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Add this to main script later for the basketball
        if (collision.gameObject.tag == "Floor")
        {
            jumpIsReady = true;
        }

    }
}
