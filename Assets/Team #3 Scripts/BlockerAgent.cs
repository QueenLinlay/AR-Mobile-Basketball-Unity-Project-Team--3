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

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        transform.position = new Vector3(2.23861051f, -1.62899995f, 1.52878571f);
        jumpIsReady = true;
    }

    public Transform Target;
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
        sensor.AddObservation(Target.localPosition);
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

        switch (moveX)
        {
            case 0: addForce.x = 0f; break;
            case 1: addForce.x = -0.5f; break;
            case 2: addForce.x = +0.5f; break;
        }

        switch (moveZ)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -0.5f; break;
            case 2: addForce.z = +0.5f; break;
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

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);


        // Reached target
        if (distanceToTarget < 0.42f)
        {
            AddReward(0.5f);
            EndEpisode();
        }
        else if (distanceToTarget > 1.2f)
        {
            AddReward(-0.5f);
            EndEpisode();
        }
        // Fell off platform
        else if (this.transform.localPosition.y < -5)
        {
            AddReward(-0.5f);
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
        else
        {

            Debug.Log("AI GameObject has collide with other GameObject... will not destroy them..");
            //AddReward(-0.1f);
            //EndEpisode();
        }

    }
    void Jump(float temp)
    {
        if (jumpIsReady)
        {
            AddReward(0.1f);
            forceJump = temp;
            rBody.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            jumpIsReady = false;
        }
    }
}
