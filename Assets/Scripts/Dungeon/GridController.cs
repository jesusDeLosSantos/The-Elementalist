using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    #region Attributes
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset;
    }
    public Room room;
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Awake()</header>
    ///     <description>This method initializes the attributes and generate the grid</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.Width - 2;
        grid.rows = room.Height - 2;
        GenerateGrid();
    }


    /// <summary>
    ///     <header>public void GenerateGrid()</header>
    ///     <description>This method generates a grid, generating in it a GameObject in each cell</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void GenerateGrid()
    {
        grid.verticalOffset += room.GetComponent<Transform>().localPosition.y;
        grid.horizontalOffset += room.GetComponent<Transform>().localPosition.x;

        for(int y = 0; y < grid.rows; y++)
        {
            for(int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows - grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                availablePoints.Add(go.transform.position);
            }
        }

        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawning();
    }
    #endregion
}
