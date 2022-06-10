using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Attributes
    public float speed;
    Rigidbody2D body;
    public GameObject bulletPre;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method gets the body of the component</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method controls the movement of the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");                 
        float vertical = Input.GetAxis("Vertical");                     

        body.velocity = new Vector3(horizontal * speed, vertical * speed, 0); //Asigna la velocidad del jugador
    }


    /// <summary>
    ///     <header>void Shoot(float x, float y)</header>
    ///     <description>This method creates a bullet and gives it its behaviour</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation) as GameObject;     

        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;                                    
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3                               
            (
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
            );
    }
    #endregion
}
