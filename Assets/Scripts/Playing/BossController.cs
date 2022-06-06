using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Idle;
    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    public bool notInRoom = false;
    public GameObject bulletPre;
    private bool coolDownAttack = false;
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomDir;
    public int life = 30;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        //Actualiza el estado del enemigo si está en la sala
        if (!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)      //Si está en rango y no muerto
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)    //Si no está en rango y no muerto
            {
                currState = EnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)  //Si está en en un vector con una distancia menor al rango de ataque
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    //Este método calcula un vector que muestra la distancia que hay con el jugador
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    //Este método selecciona direcciones aleatorias para trazar vectores de distancia y girar al enemigo lo necesario para su movimiento, dependiendo de la dirección que tome
    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    //Mantiene al enemigo moviendose aleatoriamente hasta que esté a rango y empiece a seguir
    void Wander()
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

    //Va transformando la posición del enemigo trazando un vector hacia el jugador y acercando al enemigo
    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //
    void Attack()
    {
        if (!coolDownAttack)
        {
            StartCoroutine(CoolDown());
            GameObject bullet = Instantiate(bulletPre, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<EnemyBulletController>().GetPlayer(player.transform);
        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        life -= 1;
    }
}
