using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Este método elimina el objeto cuando el jugador colisiona con él
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameController.HealPlayer(1);
            GameController.MoveSpeedChange(1);
            GameController.FireRateChange(1);
            Destroy(gameObject);
        }
    }
}
