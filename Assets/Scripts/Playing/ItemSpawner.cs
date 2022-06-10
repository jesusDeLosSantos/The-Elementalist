using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public struct Spawnable
    {
        public GameObject gameObject;
        public float weight;
    }
    
    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;
    #endregion


    #region Methods
    /// <summary>
    ///     <header>void Awake()</header>
    ///     <description>This method adds all the weight for each spawnalbe item</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Awake()
    {
        totalWeight = 0;
        foreach(var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }


    /// <summary>
    ///     <header>void Start()</header>
    ///     <description>This method generates items while the pick is higher than all the weight and while there is available positions in the index</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while(pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;

    }
    #endregion
}
