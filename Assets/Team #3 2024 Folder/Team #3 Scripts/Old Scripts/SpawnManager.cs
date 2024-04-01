using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;
    GameObject SpawnNet;

    public Camera arCam;
    public GameObject spawnedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;
        Debug.Log("Just went by if statement of touchcount");
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
        Debug.Log("About to start reaching if statement, which is below");

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            Debug.Log("Finish If statement 1");
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null)
            {
                Debug.Log("Finish If statement 2");
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Finish If statement 3");
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        Debug.Log("Finish If statement 4");
                        spawnedObject = hit.collider.gameObject; 
                    }
                    else
                    {
                        Debug.Log("Finish If statement 1-3 but at else");
                        SpawnPrefab(m_Hits[0].pose.position);
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                Debug.Log("Finish If statement 1 but at else if 1");
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log("Finish If statement 1 but at 2nd if statment");
                spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        Debug.Log("Has Spawn an object from pre-fab");
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
    }
}
