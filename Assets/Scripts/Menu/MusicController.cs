using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static bool awake= false;

    /// <summary>
    ///     <header>void Awake()</header>
    ///     <description>This method destroy or not destroy the audio in function if there is another awake</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    void Awake()
    {
        if (!awake) 
        { 
            awake = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }
}
