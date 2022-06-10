using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    #region Attributes
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame= false;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method pauses or resumes the game if you press escape key</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
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


    /// <summary>
    ///     <header>public void Pause()</header>
    ///     <description>This method pauses the game</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void Pause()
    {
        pausedGame = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }


    /// <summary>
    ///     <header>public void Resume()</header>
    ///     <description>This method resumes the game</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void Resume()
    {
        pausedGame = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }


    /// <summary>
    ///     <header>public void Quit()</header>
    ///     <description>This method loads the main menu scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

}
