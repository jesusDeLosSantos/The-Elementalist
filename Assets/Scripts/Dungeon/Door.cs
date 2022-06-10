using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Enum
    public enum DoorType
    {
        left, right, top, bottom
    }
    #endregion

    #region Attributes
    public DoorType doorType;
    public GameObject doorCollider;
    private GameObject player;
    private float widthOffset = 4f;
    #endregion

    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method establishes the GameObject to the Player object</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    /// <summary>
    ///     <header>void OnTriggerEnter2D(Collider2D other)</header>
    ///     <description>This method controls the collision of the player with the doors</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (doorType)
            {
                case DoorType.bottom:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    break;
                case DoorType.left:
                    player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y);
                    break;
                case DoorType.right:
                    player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                    break;
                case DoorType.top:
                    player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    break;
            }
        }
    }
    #endregion
}