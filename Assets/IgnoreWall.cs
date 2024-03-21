using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWall : MonoBehaviour
{
    // Start is called before the first frame update
    // Add this to main ball once done being used.
    void Start()
    {
        GameObject Wall = GameObject.FindGameObjectWithTag("FrontWall");
        GameObject SecondWall = GameObject.FindGameObjectWithTag("SecondWall");

        Physics.IgnoreCollision(Wall.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(SecondWall.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
