using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    #region Attributes
    private Camera cam;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    public float lifeTime;
    public bool isEnemyBullet = false;
    #endregion

    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method controls the behaviour of the bullet, giving it direction, rotation, time-life...</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        StartCoroutine(DeathDelay());
    }


    /// <summary>
    ///     <header>private void OnTriggerEnter2D(Collider2D col)</header>
    ///     <description>This method destroys the bullet when it collides with an enemy, a wall or the boss</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if (col.tag == "Boss")
        {
            col.gameObject.GetComponent<BossController>().Death();
            Destroy(gameObject);
        }

        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
        
    }


    /// <summary>
    ///     <header>private IEnumerator DeathDelay()</header>
    ///     <description>This method destroys the bullet after an established time</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    #endregion
}
