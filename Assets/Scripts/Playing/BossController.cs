using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    #region Attributes
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    public GameObject bulletPre;
    private bool coolDownAttack = false;
    private bool chooseDir = false;
    private Vector3 randomDir;
    public int life = 50;
    #endregion

    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method establishes the GameObject to the Player object</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method controls the status of the boss</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Victory");
        }


        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

       
            if (IsPlayerInRange(range) && currState != EnemyState.Die)      
            {
                currState = EnemyState.Follow;
            }else 
                if (!IsPlayerInRange(range) && currState != EnemyState.Die)    
                {
                    currState = EnemyState.Wander;
                }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)  
            {
                currState = EnemyState.Attack;
            }
    }


    /// <summary>
    ///     <header>private bool IsPlayerInRange(float range)</header>
    ///     <description>This method calculates the distance between it and the player</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }


    /// <summary>
    ///     <header>private IEnumerator ChooseDirection()</header>
    ///     <description>This method calculates a random direction and rotates the enemy</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }


    /// <summary>
    ///     <header>private void Wander()</header>
    ///     <description>This method maintains the enemy moving randomly until the player enters to its range</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if (IsPlayerInRange(range))
        {
            currState = EnemyState.Follow;
        }
    }


    /// <summary>
    ///     <header>private void Follow()</header>
    ///     <description>This method calculates the direction between it and the player and transform its position</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    /// <summary>
    ///     <header>private void Attack()</header>
    ///     <description>This method shoots a bullet to the player position</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void Attack()
    {
        if (!coolDownAttack)
        {
            StartCoroutine(CoolDown());
            GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<EnemyBulletController>().GetPlayer(player.transform);
        }
    }


    /// <summary>
    ///     <header>private IEnumerator CoolDown()</header>
    ///     <description>This method makes a simulation of cooldown, stopping the process for seconds</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }


    /// <summary>
    ///     <header>public void Death()</header>
    ///     <description>This method substracs 1 of life to the boss</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void Death()
    {
        life -= 1;
    }
    #endregion
}
