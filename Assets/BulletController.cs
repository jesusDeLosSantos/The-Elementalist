using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        /*if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }*/
    }


    void Update()
    {
        /*if (isEnemyBullet)
        {
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if (curPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = curPos;
        }*/
    }


    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
    }

    //Este trigger recoge la colisi�n y llama a muerte
    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<EnemyController>().Death();
    }

    //Este m�todo controla el tiempo de espera para destruir la bala, y la destruye (o se supone)
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        //Destroy(gameObject);
    }
}

