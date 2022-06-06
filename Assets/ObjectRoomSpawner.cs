using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string Name;
        public SpawnerData spawnerData;
    }



    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInChildren<GridController>();
    }

    public GridController grid;
    public RandomSpawner[] randomSpawner;

    public void InitialiseObjectSpawning()
    {
        foreach (RandomSpawner rs in randomSpawner)
        {
            SpawnObjects(rs);
        }
    }

    void SpawnObjects(RandomSpawner rs)
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
}
