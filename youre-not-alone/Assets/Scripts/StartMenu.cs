using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    public void LoadStartLevel()
    {   
        StartCoroutine(LoadLevel1());
    }

    IEnumerator LoadLevel1()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

