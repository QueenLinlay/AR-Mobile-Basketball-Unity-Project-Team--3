using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BlockerAgent : Agent
{
    Rigidbody rBody;

    public float forceMultiplier = 5f;
    public float forceJump;
    public bool jumpIsReady;
    public bool AllowToJump;

    public float fallMutiplier = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        transform.position = new Vector3(2.23861051f, -1.62899995f, 1.52878571f);
        jumpIsReady = true;
    }
    public GameObject Target;
    public GameObject MainSpawnTarget;
    public override void OnEpisodeBegin()
    {

        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < -1.852f)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(2.23861051f, -1.62899995f, 1.52878571f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        Target = GameObject.FindWithTag("Basketball");

        Debug.Log("Target Local Position z");
        Debug.Log(Target.transform.localPosition.z);

        if (rBody.velocity.y < 0)
        {
            rBody.velocity += Vector3.up * Physics.gravity.y * (fallMutiplier - 1) * Time.deltaTime;
        }

       // Debug.Log("Distance To Target");
       // Debug.Log(distanceToTarget);
       // Debug.Log("localPosition of this AI");
       // Debug.Log(this.transform.localPosition);
       // Debug.Log("localPosition of Target");
       // Debug.Log(Target.transform.localPosition);

        sensor.AddObservation(Target.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        //Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);
        sensor.AddObservation(rBody.velocity.z);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        //Vector3 controlSignal = Vector3.zero;
        float moveX = actionBuffers.DiscreteActions[0];
        //controlSignal.x = actionBuffers.ContinuousActions[0]; // 0 = don't move; 1 = left; 2 = right
        float moveZ = actionBuffers.DiscreteActions[1];
        //controlSignal.y = actionBuffers.ContinuousActions[1]; // 0 = dont move; 1 = back; 2 = forward
        float moveY = actionBuffers.DiscreteActions[2];
        //controlSignal.z = actionBuffers.ContinuousActions[2] still defining - 0 = do nothing; 1 = jump once;
        //rBody.AddForce(controlSignal * forceMultiplier);
        Vector3 addForce = new Vector3(0, 0, 0);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.transform.localPosition);

        Debug.Log("Distance To Target");
        Debug.Log(distanceToTarget);

        if (this.transform.localPosition.y < -1.4f && distanceToTarget > 0.0f)
        {
            Debug.Log("Is moving X");
            switch (moveX)
            {
                
                case 0: addForce.x = 0f; break;
                case 1: addForce.x = -0.5f; break;
                case 2: addForce.x = +0.5f; break;
            }
            Debug.Log("Is moving Z");
            switch (moveZ)
            {
                case 0: addForce.z = 0f; break;
                case 1: addForce.z = -0.5f; break;
                case 2: addForce.z = +0.5f; break;
            }
        }

        switch (moveY)
        {
            case 0: Jump(40); Debug.Log("Force Jump = 40"); break;
            case 1: Jump(50); Debug.Log("Force Jump = 50"); break;
            case 2: Jump(60); Debug.Log("Force Jump = 60"); break;
            case 3: Jump(70); Debug.Log("Force Jump = 70"); break;
        }

        //Speed
        rBody.velocity = addForce * forceMultiplier;






        if (Target.transform.localPosition.z > 1.0f)
        {
            AddReward(0.1f);
            AllowToJump = true;
        }
        else
        {
            //AddReward(0.1f);
            AllowToJump = false;
        }

        if (this.transform.localPosition.z > MainSpawnTarget.transform.localPosition.z || this.transform.localPosition.z < MainSpawnTarget.transform.localPosition.z)
        {
            AddReward(-0.1f);
        }

        if (this.transform.localPosition.x > MainSpawnTarget.transform.localPosition.x || this.transform.localPosition.x < MainSpawnTarget.transform.localPosition.x)
        {
            AddReward(-0.1f);
        }


            // Reached target
            //if (distanceToTarget < 0.30f)
            //{
            //AddReward(0.1f);
            // EndEpisode();
            //}

            if (distanceToTarget > 0.0f)
        {
            AddReward(-0.1f);
        }
        // Fell off platform
        else if (this.transform.localPosition.y < -5)
        {
            AddReward(-1f);
            EndEpisode();
        }
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");

    }


    // When collider is enter (the ball that is shooting into the net) do something (can affect others collider
    private void OnCollisionEnter(Collision collision)
    {
        //Add this to main script later for the basketball
        if (collision.gameObject.tag == "Floor")
        {
            jumpIsReady = true;
        }

        if (collision.gameObject.tag == "Basketball")
        {
            //spawn.Check = false;
            Destroy(collision.gameObject);
            Debug.Log("AI Collide with BasketBall Reward");
            AddReward(1.0f);
            EndEpisode();
            //Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
        
        if(collision.gameObject.tag == "TopWall" || collision.gameObject.tag == "RightWall" || collision.gameObject.tag == "LeftWall")
        {
            Debug.Log("collide with either Top, Right, Left Wall");
            AddReward(-1.0f);
            EndEpisode();
        }


    }
    void Jump(float temp)
    {
        if (jumpIsReady && AllowToJump)
        {
            AddReward(0.1f);
            forceJump = temp;
            rBody.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            jumpIsReady = false;
        }
    }
}
