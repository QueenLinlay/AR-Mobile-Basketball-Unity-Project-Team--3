using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;


public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public InputField playerNameInput;

    private bool isPaused = false;

    void Start()
    {
        // Hide the pause menu canvas initially
        PauseMenuCanvas.SetActive(false);
    }

    void Update()
    {
        // Check for pause input (e.g., pressing the "Escape" key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;

        // Show the pause menu canvas
        PauseMenuCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the pause menu canvas
        PauseMenuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        // Here you would start the game and pass the player's name to the game manager or another script
        string playerName = playerNameInput.text;
        Debug.Log("Starting game with player name: " + playerName);

        // Resume the game
        ResumeGame();
    }
}
