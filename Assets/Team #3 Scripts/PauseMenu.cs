using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject mainCanvas; 
    public InputField playerNameInput;

    private bool isPaused = false;

    void Start()
    {
        // Hide the pause menu canvas initially
        pauseMenuCanvas.SetActive(false);
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
        pauseMenuCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;

        // Hide the pause menu canvas
        pauseMenuCanvas.SetActive(false);
        mainCanvas.SetActive(true); 
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
    public void QuitGame()
    {
        // Quit the application (for development purposes)
        UnityEngine.Application.Quit(); // Specify UnityEngine.Application
        Application.Quit(); 
    }

    public void StartGame()
    {
        // Here you would start the game and pass the player's name to the game manager or another script
        string playerName = playerNameInput.text;
        UnityEngine.Debug.Log("Starting game with player name: " + playerName); // Specify UnityEngine.Debug
        // Resume the game
        ResumeGame();
    }
}
