using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class gotohome : MonoBehaviour
{
    // This function will be triggered by our Play Button
    void Start()
    {
        // Force the mouse to unlock and show up so you can click the button
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void HomeScreen()
    {
        SceneManager.LoadScene("MenuScene");
    }
}