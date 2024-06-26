using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreForRewards : MonoBehaviour
{
    // Basic GameObject or access to script names
    //public GameObject ball;
    //public SpawnBall spawn;
    // public GameObject Holder;
    BlockerAgent Block;
    GameObject AI;
    // UI Tert GameObjects
    [SerializeField]
    public GameObject ScoreHolder;

    // Text Components
    [SerializeField]
    TextMeshProUGUI ScoreText;
    //Update Later to see score.
    int Score = 0;

    // Start is called before the first frame update
    private void Start()
    {
        // Making sure we have access to SpawnBall Component for the bool global variable
        //Holder = GameObject.FindWithTag("SpawnBall");
        //spawn = Holder.GetComponent<SpawnBall>();
        AI = GameObject.FindWithTag("AI");
        Block = AI.GetComponent<BlockerAgent>();
        ScoreText = ScoreHolder.GetComponent<TextMeshProUGUI>();
    }
    
    // When collider is enter (the ball that is shooting into the net) do something (can affect others collider
    private void OnCollisionEnter(Collision collision)
    {
        //Add this to main script later for the basketball
        if (collision.gameObject.tag == "Basketball")
        {
            //spawn.Check = false;
            Destroy(collision.gameObject);
            //Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
            Score++;
            ScoreText.text = Score.ToString();
            Block.AddReward(-0.2f);
            Debug.Log("Scored!! AI Failed to block");
            //Debug.Log(Score);
        }
        else
        {
            Debug.Log("Other GameObject has collide with this... will not be destroy..");
        }

    }
}
