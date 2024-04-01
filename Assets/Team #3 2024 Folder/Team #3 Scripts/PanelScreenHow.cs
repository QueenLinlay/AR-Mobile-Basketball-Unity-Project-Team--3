using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScreenHow : MonoBehaviour
{
    public GameObject Change;
    public GameObject PanelHowTo;

    public void ChangeSceneTrue()
    {
        Change.SetActive(false);
        PanelHowTo.SetActive(true);
    }
    public void ChangeSceneFalse()
    {
        Change.SetActive(true);
        PanelHowTo.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
