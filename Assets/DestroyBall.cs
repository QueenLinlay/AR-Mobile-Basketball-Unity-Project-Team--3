using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{

    // When collider is enter (the ball that is shooting into the net) do something (can affect others collider
    private void OnCollisionEnter(Collision collision)
    {
        //Add this to main script later for the basketball
        if (collision.gameObject.tag == "Basketball")
        {
            //spawn.Check = false;
            Destroy(collision.gameObject);
            Debug.Log("AI Collide with BasketBall Reward");
            //Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
        }
        else
        {
            Debug.Log("AI GameObject has collide with other GameObject... will not destroy them..");
        }

    }
}
