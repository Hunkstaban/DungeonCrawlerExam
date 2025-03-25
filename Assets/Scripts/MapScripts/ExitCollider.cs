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
            GameManager.Instance.UpdateRoomRecord();
            
            var frontRoom = roomGenerator.GetFrontRoom();
            var middleRoom = roomGenerator.GetMiddleRoom();

            if (frontRoom == null)
            {
                Transform exitTransform = middleRoom.exitPlane.transform;
                Vector3 spawnPosition = exitTransform.position + exitTransform.forward * (roomGenerator.GetSegmentLength(exitTransform) / 2);
                Quaternion spawnRotation = Quaternion.LookRotation(exitTransform.forward, Vector3.up);
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, spawnRotation);
            }
            else
            {
                // 1. Destroy the back room before proceeding
                GameObject backRoom = roomGenerator.GetBackRoom()?.gameObject;
                if (backRoom != null)
                {
                    PowerUpSpawner spawner = backRoom.GetComponent<PowerUpSpawner>();
                    if (spawner != null) spawner.RemovePowerUps();
                    Destroy(backRoom);
                    Debug.Log("Destroying back room: " + backRoom.name);
                }

                roomGenerator.SetBackRoom(null); // Clear reference

                // 2. Move room references forward
                roomGenerator.SetBackRoom(middleRoom);
                roomGenerator.SetMiddleRoom(frontRoom);

                // 3. Generate the new front room
                Transform exitTransform = roomGenerator.GetMiddleRoom().exitPlane.transform;

                Vector3 spawnPosition = exitTransform.position + exitTransform.forward * (roomGenerator.GetSegmentLength(exitTransform) / 2);
                Quaternion spawnRotation = Quaternion.LookRotation(exitTransform.forward, Vector3.up);
                
                frontRoom = roomGenerator.GetRandomRoom();
                frontRoom = Instantiate(frontRoom, spawnPosition, spawnRotation);
            }

            roomGenerator.SetFrontRoom(frontRoom);
            Destroy(gameObject);
        }
    }
}