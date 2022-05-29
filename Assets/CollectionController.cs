using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class CollectionController : MonoBehaviour
{
    Random rand = new Random();

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
            
            switch (rand.Next(0, 4))
            {
                case 1:
                    GameController.HealPlayer(1);
                    break;
                case 2:
                    GameController.MoveSpeedChange(1);
                    break;
                case 3:
                    GameController.FireRateChange();
                break;
            }
            Destroy(gameObject);
        }
    }
}
