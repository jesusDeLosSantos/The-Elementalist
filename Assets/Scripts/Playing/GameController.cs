using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 10;
    private static int maxHealth = 15;
    private static float moveSpeed = 5f;
    private static float fireRate = 2;
    public Text healthText;
    public Text speedText;
    public Text fireRateText;


    //Getters y setters
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }



    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        speedText.text = "Speed: " + moveSpeed;
        fireRateText.text = "FireRate: " + fireRate + "s";
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange()
    {
        fireRate = fireRate/2;
    }



    private static void KillPlayer()
    {
        SceneManager.LoadScene("Defeat");
    }
}