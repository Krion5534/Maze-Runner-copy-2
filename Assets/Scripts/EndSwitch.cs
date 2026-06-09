using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTrigger : MonoBehaviour
{
    [Tooltip("The exact name of the scene you want to load.")]
    public string sceneToLoad;

    [Tooltip("The tag assigned to your player capsule.")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag(playerTag))
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}