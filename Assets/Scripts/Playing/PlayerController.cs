using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D body;
    public GameObject bulletPre;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");                 //Para controlar el movimiento horizontal
        float vertical = Input.GetAxis("Vertical");                     //Para controlar el movimiento vertical

        body.velocity = new Vector3(horizontal * speed, vertical * speed, 0); //Asigna la velocidad del jugador
    }

    
    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPre, transform.position, transform.rotation) as GameObject;     //Creo la bala

        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;                                    
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3                               //Creo un vector a raíz de los parámetros de disparo vertical y horizontal, redondeandolos hacia arriba o hacia abajo, según si son mayores a 0
            (
                (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
                (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
                0
            );
    }

    

}
