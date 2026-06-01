using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 3000.0f;

    [SerializeField]
    private string sceneNameToLoad;

    public void TriggerSceneLoad()
    {
        Invoke("LoadScene", delayBeforeLoading);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}