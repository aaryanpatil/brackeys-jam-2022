using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isMute = false;
    [SerializeField] float reloadLevelDelay = 0.25f;

    public static PauseMenu instance;
    void Awake()
    {
        GetComponentInChildren<Canvas>().enabled = isPaused;
    }
    public void DisplayPauseMenu()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        GetComponentInChildren<Canvas>().enabled = isPaused;
    }

    public void MainMenu()
    {
        isPaused = false;
        SceneManager.LoadScene("Start Menu");
        GetComponentInChildren<Canvas>().enabled = isPaused;
        Time.timeScale = 1;
    }

    public void RestartGameFromPause()
    {
        isPaused = false;
        GetComponentInChildren<Canvas>().enabled = isPaused;
        Time.timeScale = 1;
        StartCoroutine(ReloadLevel());
    }
     IEnumerator ReloadLevel()
     {
        yield return new WaitForSecondsRealtime(reloadLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
     }
}
