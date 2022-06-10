using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /// <summary>
    ///     <header>public void LoadGame()</header>
    ///     <description>This method loads game scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    ///     <header>public void LoadSettings()</header>
    ///     <description>This method loads settings scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    /// <summary>
    ///     <header>public void CloseGame()</header>
    ///     <description>This method close the game</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    ///     <header>public void LoadMainMenu()</header>
    ///     <description>This method loads main menu scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
