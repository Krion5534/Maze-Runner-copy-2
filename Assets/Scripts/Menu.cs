using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class MainMenu : MonoBehaviour
{
    // This function will be triggered by our Play Button
    public void PlayGame()
    {
        // Loads the scene named "GameScene". 
        // Make sure this matches the exact name of your maze scene!
        SceneManager.LoadScene("SampleScene");
    }

    // Optional: Call this function if you add a "Quit" button later
    public void QuitGame()
    {
        Debug.Log("Game Exited!");
        Application.Quit(); // Closes the running game build application
    }
}