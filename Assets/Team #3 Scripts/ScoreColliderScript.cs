using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreColliderScript : MonoBehaviour
{
    // Basic GameObject or access to script names
    public GameObject ball;
    public SpawnBall spawn;
    public GameObject Holder;

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
        Holder = GameObject.FindWithTag("SpawnBall");
        spawn = Holder.GetComponent<SpawnBall>();
        ScoreText = ScoreHolder.GetComponent<TextMeshProUGUI>();
    }
    
    // When collider is enter (the ball that is shooting into the net) do something (can affect others collider
    private void OnCollisionEnter(Collision collision)
    {
        spawn.Check = false;
        Destroy(collision.gameObject);
        Instantiate(ball, new Vector3(0f, 1f, 0f), Quaternion.identity);
        Score++;
        ScoreText.text = Score.ToString();
        Debug.Log(Score);
    }
}
