using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public List<MapSegment> rooms;

    public MapSegment startRoom;

    public MapSegment backRoom;
    public MapSegment middleRoom;
    public MapSegment frontRoom;

    // [HideInInspector]

    private bool hasStarted = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!hasStarted)
        {
            backRoom = Instantiate(startRoom, Vector3.zero, Quaternion.identity);

            // Get the exit plane of the first room
            Transform exitTransform = backRoom.exitPlane.transform;

            // Calculate the correct spawn position at the end of the exitPlane
            Vector3 spawnPosition = exitTransform.position + exitTransform.forward * (GetSegmentLength(backRoom) / 2);

            // Instantiate the next room at the correct position
            middleRoom = Instantiate(GetRandomRoom(), spawnPosition, Quaternion.identity);

            hasStarted = true;
        }
    }

// Corrected GetSegmentLength method
    public float GetSegmentLength(MapSegment segment)
    {
        Transform planeTransform = segment.exitPlane.transform;
        if (planeTransform != null)
        {
            MeshRenderer renderer = planeTransform.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                return renderer.bounds.size.z; // Use bounds instead of localScale
            }
        }
        
        return 0f;
    }
    
    public MapSegment GetRandomRoom()
    {
        int randomIndex = UnityEngine.Random.Range(0, rooms.Count);
        return rooms[randomIndex];
    }
    
    public void SetFrontRoom(MapSegment room)
    {
        frontRoom = room;
    }
    
    public void SetMiddleRoom(MapSegment room)
    {
        middleRoom = room;
    }
    
    public void SetBackRoom(MapSegment room)
    {
        backRoom = room;
    }
    
    public MapSegment GetFrontRoom()
    {
        return frontRoom;
    }
    
    public MapSegment GetMiddleRoom()
    {
        return middleRoom;
    }
    
    public MapSegment GetBackRoom()
    {
        return backRoom;
    }
}
