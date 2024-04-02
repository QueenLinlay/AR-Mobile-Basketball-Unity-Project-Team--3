using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MainUI;
    public GameObject PauseMenu;

    private void Start()
    {
        MainUI.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void ActivePause()
    {
        Time.timeScale = 0;

        MainUI.SetActive(false);
        PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

        MainUI.SetActive(true);
        PauseMenu.SetActive(false);
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneBuildIndex:0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
