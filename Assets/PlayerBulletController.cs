using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float lifeTime;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Este método controla el tiempo de espera para destruir la bala, y la destruye (o se supone)
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        //Destroy(gameObject);
    }

}
