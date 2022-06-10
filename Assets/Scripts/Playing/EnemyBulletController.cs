using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    #region Attributes
    public float lifeTime;
    public bool isEnemyBullet = true;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method starts the destruction of the bullet</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method updates the position of the bullet. It the position is the same, it destroys itself</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        curPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
        if (curPos == lastPos)
        {
            Destroy(gameObject);
        }
        lastPos = curPos;
    }

    /// <summary>
    ///     <header>public void GetPlayer(Transform player)</header>
    ///     <description>This method takes the position of the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }


    /// <summary>
    ///     <header>private IEnumerator DeathDelay()</header>
    ///     <description>This method creates a delay for the destruction of the bullet</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }


    /// <summary>
    ///     <header>private void OnTriggerEnter2D(Collider2D col)</header>
    ///     <description>This method deletes the bullet when the player or the walls collide</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }

        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
