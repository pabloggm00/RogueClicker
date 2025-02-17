using System;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public static RoomGenerator instance;

    public Room[] rooms;

    public static Action OnEnemyDeath;

    [Serializable]
    public class Room
    {
        public RoomType type;
        public GameObject roomObject;
        public float probability; // Probabilidad de aparición (en porcentaje)
    }

    private void Awake()
    {
        instance = this;
    }

    public void DisableAll()
    {
        foreach (var room in rooms)
        {
            room.roomObject.SetActive(false); // Desactiva todas las salas
        }

        OnEnemyDeath?.Invoke();
    }

    public void DisableForPowerUp()
    {

        DisableAll();
    }

    public void CreateRoom()
    {

        if (GameplayController.instance.currentRoom == 1)
        {
            foreach (Room room in rooms)
            {
                if (room.type == RoomType.Enemy)
                {
                    ActivateRoom(room);
                    return;
                }
            }
        }

        float totalProbability = 0f;
        foreach (var room in rooms)
        {
            totalProbability += room.probability;
        }

        float randomPoint = UnityEngine.Random.Range(0f, totalProbability);
        float cumulative = 0f;

        foreach (var room in rooms)
        {
            cumulative += room.probability;
            if (randomPoint <= cumulative)
            {
                ActivateRoom(room);
                break;
            }
        }
    }

    void ActivateRoom(Room selectedRoom)
    {
       DisableAll();

        selectedRoom.roomObject.SetActive(true); // Activa la sala elegida

        switch (selectedRoom.type)
        {
            case RoomType.Enemy:
                //restamos 3 fuerza
                break;
            case RoomType.Chest:
                //restamos
                break;
            case RoomType.Event:
                break;
            default:
                break;
        }

        if (selectedRoom.roomObject.TryGetComponent<EnemyRoom>(out EnemyRoom enemyRoom))
        {
            enemyRoom.CreateEnemy();    
        }
    }
}

public enum RoomType
{
    Enemy,
    Chest,
    Event
}
