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

            // Get segment length for proper positioning
            float segmentLength = GetSegmentLength(exitTransform);

            // Calculate spawn position
            Vector3 spawnPosition = exitTransform.position + exitTransform.forward * (segmentLength / 2);

            // Get a random room to instantiate
            MapSegment nextRoom = GetRandomRoom();

            // Rotate the new room to match the exit plane's forward direction
            Quaternion spawnRotation = Quaternion.LookRotation(exitTransform.forward, Vector3.up);

            // Instantiate the room with correct position and rotation
            middleRoom = Instantiate(nextRoom, spawnPosition, spawnRotation);

            hasStarted = true;
        }
    }

// Corrected GetSegmentLength method
    public float GetSegmentLength(Transform exitTransform)
    {
        Transform planeTransform = exitTransform.transform;
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
