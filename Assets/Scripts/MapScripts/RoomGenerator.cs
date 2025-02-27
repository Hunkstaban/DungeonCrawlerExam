using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public List<GameObject> rooms;

    public GameObject startRoom;
    
     public GameObject backRoom;
     public GameObject middleRoom;
     public GameObject frontRoom;
     
     // [HideInInspector]

    private static bool hasStarted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!hasStarted)
        {
            backRoom = startRoom;
            middleRoom = GetRandomRoom();
            backRoom = Instantiate(backRoom, new Vector3(0, 0, 0), Quaternion.identity);
            
            Vector3 spawnPosition = backRoom.transform.position + backRoom.transform.forward * GetSegmentLength(backRoom);
            middleRoom = Instantiate(middleRoom, spawnPosition, Quaternion.identity);
            hasStarted = true;
        }
        
    }
    
    public float GetSegmentLength(GameObject segment)
    {
        Transform planeTransform = segment.transform.Find("Plane");
        if (planeTransform != null)
        {
            MeshRenderer renderer = planeTransform.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                return renderer.bounds.size.z; // Adjust if using a different axis
            }
        }
        return 0f;
    }
    
    public GameObject GetRandomRoom()
    {
        int randomIndex = UnityEngine.Random.Range(0, rooms.Count);
        return rooms[randomIndex];
    }
    
    public void SetFrontRoom(GameObject room)
    {
        frontRoom = room;
    }
    
    public void SetMiddleRoom(GameObject room)
    {
        middleRoom = room;
    }
    
    public void SetBackRoom(GameObject room)
    {
        backRoom = room;
    }
    
    public GameObject GetFrontRoom()
    {
        return frontRoom;
    }
    
    public GameObject GetMiddleRoom()
    {
        return middleRoom;
    }
    
    public GameObject GetBackRoom()
    {
        return backRoom;
    }
}
