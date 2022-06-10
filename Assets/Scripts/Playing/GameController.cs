using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    #region Attributes
    public static GameController instance;
    private static float health = 10;
    private static int maxHealth = 15;
    private static float moveSpeed = 5f;
    private static float fireRate = 2;
    public Text healthText;
    public Text speedText;
    public Text fireRateText;
    #endregion


    #region Getters and setters
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    #endregion


    #region Methods
    /// <summary>
    ///     <header>private void Awake()</header>
    ///     <description>This method instantiates the statistics of the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void Awake()
    {
        health = 10;
        moveSpeed = 5f;
        fireRate = 2;

        if (instance == null)
        {
            instance = this;
        }
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method updates the texts of the statistics of the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        healthText.text = ": " + health;
        speedText.text = ": " + moveSpeed;
        fireRateText.text = ": " + Math.Round(fireRate, 2) + "s";
    }


    /// <summary>
    ///     <header>public static void DamagePlayer(int damage)</header>
    ///     <description>This method rest health to the player and kills it if it is necessary</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }


    /// <summary>
    ///     <header>public static void HealPlayer(float healAmount)</header>
    ///     <description>This method heals the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }


    /// <summary>
    ///     <header>public void Death()</header>
    ///     <description>This method rest 1 of life to the boss</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }


    /// <summary>
    ///     <header>public static void FireRateChange()</header>
    ///     <description>This method divides the fire rate of the player by 2</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public static void FireRateChange()
    {
        fireRate = fireRate/2;
    }


    /// <summary>
    ///     <header>private static void KillPlayer()</header>
    ///     <description>This method loads the lose scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private static void KillPlayer()
    {
        SceneManager.LoadScene("Defeat");
    }
    #endregion
}