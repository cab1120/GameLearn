using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class ConnectedController3 : MonoBehaviour
{
    public static ConnectedController3 instance;
    public Transform MaxRoomPlayer,MaxRoomInteractor;
    public Transform MinRoomPlayer,MinRoomInteractor;
    public Transform MiddleRoomPlayer,MiddleRoomInteractor;

    public Transform MaxRoomRoot;
    public Transform MinRoomRoot;
    public Transform MiddleRoomRoot;

    public enum State { MaxRoom, MinRoom, MidlleRoom, }
    public State currentState = State.MidlleRoom;

    private float scale = 0.2f;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        Transform currentRoomPlayer = GetCurrentRoomPlayer();
        Transform currentRoomRoot = GetCurrentRoomRoot();
        Transform currentRoomInteractor = GetCurrentRoomInteractor();
        GetChanging(currentRoomRoot, currentRoomPlayer,currentRoomInteractor);
    }



    private void GetChanging(Transform currentRoomRoot, Transform currentRoomPlayer, Transform currentRoomInteractor)
    {
        switch (currentState)
        {
            case State.MaxRoom:
                //Debug.Log("MaxRoom");
                SyncToAll(currentRoomRoot, MiddleRoomRoot, currentRoomPlayer, MiddleRoomPlayer, scale);
                SyncToAll(currentRoomRoot,MiddleRoomRoot,currentRoomInteractor,MiddleRoomInteractor, scale);
                SyncToAll(currentRoomRoot, MinRoomRoot, currentRoomPlayer, MinRoomPlayer, scale*scale);
                SyncToAll(currentRoomRoot,MinRoomRoot,currentRoomInteractor,MinRoomInteractor, scale*scale);
                break;
            case State.MinRoom:
                //Debug.Log("MinRoom");
                SyncToAll(currentRoomRoot,MiddleRoomRoot,currentRoomPlayer,MiddleRoomPlayer, 1f/scale);
                SyncToAll(currentRoomRoot,MiddleRoomRoot,currentRoomInteractor,MiddleRoomInteractor, 1f/scale);
                SyncToAll(currentRoomRoot,MaxRoomRoot,currentRoomPlayer,MaxRoomPlayer, 1f/(scale*scale));
                SyncToAll(currentRoomRoot,MaxRoomRoot,currentRoomInteractor,MaxRoomInteractor, 1f/(scale*scale));
                break;
            case State.MidlleRoom:
                //Debug.Log("MIDLLE ROOM");
                SyncToAll(currentRoomRoot,MaxRoomRoot,currentRoomPlayer,MaxRoomPlayer, scale);
                SyncToAll(currentRoomRoot,MaxRoomRoot,currentRoomInteractor,MaxRoomInteractor, scale);
                SyncToAll(currentRoomRoot,MinRoomRoot,currentRoomPlayer,MinRoomPlayer, 1f/scale);
                SyncToAll(currentRoomRoot,MinRoomRoot,currentRoomInteractor,MinRoomInteractor, 1f/scale);
                break;
        }
    }

    private void SyncToAll(Transform currentRoomRoot, Transform targetRoomRoot, Transform currentRoomObj, Transform targetRoomObj, float scl)
    {
        Vector3 localToRoom = currentRoomRoot.InverseTransformPoint(currentRoomObj.position);
        Quaternion localToRoomRotation = Quaternion.Inverse(currentRoomRoot.localRotation)*currentRoomObj.localRotation;
        
        Vector3 scaledPos=localToRoom;
        
        Vector3 targetWorldPos = targetRoomRoot.TransformPoint(scaledPos);
        Quaternion targetWorldRot = targetRoomRoot.rotation * localToRoomRotation;
        
        targetRoomObj.position = targetWorldPos;
        targetRoomObj.rotation = targetWorldRot;
        targetRoomObj.localScale = currentRoomObj.localScale;
    }
    private Transform GetCurrentRoomInteractor()
    {
        switch (currentState)
        {
            case State.MaxRoom:
                return MaxRoomInteractor;
            case State.MidlleRoom:
                return MiddleRoomInteractor;
            case State.MinRoom:
                return MinRoomInteractor;
            default:
                return null;
        }
    }
    private Transform GetCurrentRoomRoot()
    {
        switch (currentState)
        {
            case State.MaxRoom:
                return MaxRoomRoot;
            case State.MidlleRoom:
                return MiddleRoomRoot;
            case State.MinRoom:
                return MinRoomRoot;
            default:
                return null;
        }
    }

    private Transform GetCurrentRoomPlayer()
    {
        switch (currentState)
        {
            case State.MaxRoom:
                return MaxRoomPlayer;
            case State.MidlleRoom:
                return MiddleRoomPlayer;
            case State.MinRoom:
                return MinRoomPlayer;
            default:
                return null;
        }
    }
}
