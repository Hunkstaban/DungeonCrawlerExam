using UnityEngine;

public class ExitCollider : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    
    private void Awake()
    {
        roomGenerator = FindFirstObjectByType<RoomGenerator>();
        if (roomGenerator == null)
        {
            Debug.LogError("ExitCollider: No RoomGenerator found in the scene!");
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var frontRoom = roomGenerator.GetFrontRoom();
            var middleRoom = roomGenerator.GetMiddleRoom();
        
            if (frontRoom == null)
            {
                Transform exitTransform = middleRoom.exitPlane.transform;
                Vector3 spawnPosition = exitTransform.position + exitTransform.forward * (roomGenerator.GetSegmentLength(middleRoom) / 2);
            
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, Quaternion.identity);
            }
            else
            {
                // 1. Destroy the back room before proceeding
                GameObject backRoom = roomGenerator.GetBackRoom()?.gameObject;
                if (backRoom != null)
                {
                    Destroy(backRoom);
                    Debug.Log("Destroying back room: " + backRoom.name);
                }
                roomGenerator.SetBackRoom(null); // Clear reference

                // 2. Move room references forward
                roomGenerator.SetBackRoom(middleRoom);
                roomGenerator.SetMiddleRoom(frontRoom);

                // 3. Generate the new front room
                middleRoom = roomGenerator.GetMiddleRoom();
                Vector3 spawnPosition = middleRoom.exitPlane.transform.position + middleRoom.exitPlane.transform.forward * (roomGenerator.GetSegmentLength(middleRoom) / 2);
            
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, Quaternion.identity);
            }

            roomGenerator.SetFrontRoom(frontRoom);
            Destroy(gameObject);
        }
    }
}
