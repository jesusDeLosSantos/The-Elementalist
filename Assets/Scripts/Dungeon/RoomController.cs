using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    #region Attributes
    public static RoomController instance;
    string currentWorldName = "Basement";
    RoomInfo currentLoadRoomData;
    Room currRoom;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    #endregion


    #region Methods
    void Awake()
    {
        instance = this;
    }


    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method load a room always down the starting one</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Start()
    {
        LoadRoom("Empty", 0, -1);
    }


    /// <summary>
    ///     <header>void Update()</header>
    ///     <description>This method updates constantly the room queue</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Update()
    {
        UpdateRoomQueue();
    }


    /// <summary>
    ///     <header>void UpdateRoomQueue()</header>
    ///     <description>This method counts and loads rooms in the queue, and makes sure that the last one is the boss room, deleting then the unconnected rooms</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }


    /// <summary>
    ///     <header>IEnumerator SpawnBossRoom()</header>
    ///     <description>This method spawns and controls that there is an only boss room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }


    /// <summary>
    ///     <header>public void LoadRoom(string name, int x, int y)</header>
    ///     <description>This method loads a new room if in its position doesn't exist another one</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y) == true)
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }


    /// <summary>
    ///     <header>IEnumerator LoadRoomRoutine(RoomInfo info)</header>
    ///     <description>This method loads a scene for create the next room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }


    /// <summary>
    ///     <header>public void RegisterRoom(Room room)</header>
    ///     <description>This method resgister the data of the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }


    /// <summary>
    ///     <header>public bool DoesRoomExist(int x, int y)</header>
    ///     <description>This method returns a boolean about the coordenates of the object are not null</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }


    /// <summary>
    ///     <header>public Room FindRoom(int x, int y)</header>
    ///     <description>This method returns the coordenates of the item</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }


    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method returns the start of the name of the room</description>                //En un principio podrían haber distintos tipos de salas, pero decidí dejarla en una por falta de tiempo y dejar este método pòr si algún día las implemento
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public string GetRandomRoomName()
    {
        return "Empty";
    }


    /// <summary>
    ///     <header>public void OnPlayerEnterRoom(Room room)</header>
    ///     <description>This method controls the camera when the player enter for a new room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;

        StartCoroutine(RoomCoroutine());
    }


    /// <summary>
    ///     <header>public IEnumerator RoomCoroutine()</header>
    ///     <description>This method makes a wait</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}