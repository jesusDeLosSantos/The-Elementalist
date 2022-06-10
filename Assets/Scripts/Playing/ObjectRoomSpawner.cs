using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public struct RandomSpawner
    {
        public string Name;
        public SpawnerData spawnerData;
    }
    public GridController grid;
    public RandomSpawner[] randomSpawner;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method gets the component in the scene</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInChildren<GridController>();
    }


    /// <summary>
    ///     <header>public void InitialiseObjectSpawning()</header>
    ///     <description>This method spawns an object for each random spawner we generate</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public void InitialiseObjectSpawning()
    {
        foreach (RandomSpawner rs in randomSpawner)
        {
            SpawnObjects(rs);
        }
    }


    /// <summary>
    ///     <header>private void SpawnObjects(RandomSpawner rs)</header>
    ///     <description>This method generates a random position in the grid and generates an object there</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void SpawnObjects(RandomSpawner rs)
    {
        int i = Random.Range(rs.spawnerData.minSpawn, rs.spawnerData.maxSpawn + 1);

        int randomPos;
        GameObject go;

        for (int j = 0; j < i; j++)
        {
            randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            go = Instantiate(rs.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
        }
    }
    #endregion
}
