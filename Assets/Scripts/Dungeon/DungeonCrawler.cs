using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCrawler
{
    public Vector2Int Position { get; set; }

    public DungeonCrawler(Vector2Int startPos)
    {
        Position = startPos;
    }


    /// <summary>
    ///     <header>public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)</header>
    ///     <description>This method returns the position to move the next room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public Vector2Int Move(Dictionary<Direction, Vector2Int> directionMovementMap)
    {
        Direction toMove = (Direction)Random.Range(0, directionMovementMap.Count);
        Position += directionMovementMap[toMove];
        return Position;
    }
}