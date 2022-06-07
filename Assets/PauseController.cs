using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame= false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausedGame)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        pausedGame = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    
}
