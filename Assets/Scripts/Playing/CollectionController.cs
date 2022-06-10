using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class CollectionController : MonoBehaviour
{
    #region Attributes
    Random rand = new Random();
    #endregion


    #region Methods
    /// <summary>
    ///     <header>private void OnTriggerEnter2D(Collider2D collision)</header>
    ///     <description>This method deletes the object when the player collides with the object and gives to the player a random buff.</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    //Este método elimina el objeto cuando el jugador colisiona con él
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            switch (rand.Next(0, 3))
            {
                case 0:
                    GameController.HealPlayer(1);
                    break;
                case 1:
                    GameController.MoveSpeedChange(1);
                    break;
                case 2:
                    GameController.FireRateChange();
                break;
            }
            Destroy(gameObject);
        }
    }
    #endregion
}
