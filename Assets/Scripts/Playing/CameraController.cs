using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Attributes
    public static CameraController instance;
    public Room currRoom;
    public float moveSpeedWhenRoomChange;
    #endregion


    #region Methods
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }


    /// <summary>
    ///     <header>private void UpdatePosition()</header>
    ///     <description>This method updates the position of the camera in the scene depending on the rooms</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
    }


    /// <summary>
    ///     <header>private Vector3 GetCameraTargetPosition()</header>
    ///     <description>This method gives the camera the position of the room</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    private Vector3 GetCameraTargetPosition()
    {
        Vector3 targetPos = currRoom.GetRoomCentre();
        targetPos.z = transform.position.z;

        return targetPos;
    }


    /// <summary>
    ///     <header>public bool IsSwitchingScene()</header>
    ///     <description>This method transforms the position of the camera</description>
    ///     <precondition>None</precondition>
    ///     <postcondition>None</postcondition>
    /// </summary>
    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
    #endregion
}
