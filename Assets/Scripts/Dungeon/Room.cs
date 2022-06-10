using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    #region Attributes
    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public int Width;
    public int Height;
    public int X;
    public int Y;
    private bool updatedDoors = false;
    public List<Door> doors = new List<Door>();
    #endregion


    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method asigns the doors and registers the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method removes the doors of the rooms when the boss room is created</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }


    /// <summary>
    ///     <header>public void RemoveUnconnectedDoors()</header>
    ///     <description>This method remove the door if there is not a room next to</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }


    /// <summary>
    ///     <header>public Room GetRight()</header>
    ///     <description>This method checks if there is a room at right</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Room GetRight()
    {
        Room loadedRoom = null;

        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            loadedRoom = RoomController.instance.FindRoom(X + 1, Y);
        }

        return loadedRoom;
    }


    /// <summary>
    ///     <header>public Room GetLeft()</header>
    ///     <description>This method checks if there is a room at left</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Room GetLeft()
    {
        Room loadedRoom = null;

        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            loadedRoom = RoomController.instance.FindRoom(X - 1, Y);
        }
        return loadedRoom;
    }


    /// <summary>
    ///     <header>public Room GetTop()</header>
    ///     <description>This method checks if there is a room at top</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Room GetTop()
    {
        Room loadedRoom = null;

        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            loadedRoom = RoomController.instance.FindRoom(X, Y + 1);
        }
        return loadedRoom;
    }

    /// <summary>
    ///     <header>public Room GetBottom()</header>
    ///     <description>This method checks if there is a room at bottom</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Room GetBottom()
    {
        Room loadedRoom = null;

        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            loadedRoom = RoomController.instance.FindRoom(X, Y - 1);
        }
        return loadedRoom;
    }


    /// <summary>
    ///     <header>void OnDrawGizmos()</header>
    ///     <description>This method draw red square for the container of the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }


    /// <summary>
    ///     <header>public Vector3 GetRoomCentre()</header>
    ///     <description>This method gets the centre of the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }


    /// <summary>
    ///     <header>void OnTriggerEnter2D(Collider2D other)</header>
    ///     <description>This method controls if the player enters at the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}