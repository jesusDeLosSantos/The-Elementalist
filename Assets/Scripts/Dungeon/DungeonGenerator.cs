using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    #region Attributes
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method takes all the rooms generated and generates them</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }


    /// <summary>
    ///     <header>private void SpawnRooms(IEnumerable<Vector2Int> rooms)</header>
    ///     <description>This method instances and loads the rooms from the list with their assigned location</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
    #endregion
}