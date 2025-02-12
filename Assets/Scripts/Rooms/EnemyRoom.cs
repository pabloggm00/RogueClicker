using System;
using UnityEngine;
using static RoomGenerator;

public class EnemyRoom : MonoBehaviour
{

    public Enemy[] enemies;
    public float incrementoHP;

    public static Action OnInitEnemy;

    [Serializable]
    public class Enemy
    {
        public EnemyRare rare;
        public GameObject enemyObject;
        public float probability; // Probabilidad de aparición (en porcentaje)
        public int baseHP;
    }

    public void CreateEnemy()
    {
        float totalProbability = 0f;
        foreach (Enemy enemy in enemies)
        {
            totalProbability += enemy.probability;
        }

        float randomPoint = UnityEngine.Random.Range(0f, totalProbability);
        float cumulative = 0f;

        foreach (Enemy enemy in enemies)
        {
            cumulative += enemy.probability;
            if (randomPoint <= cumulative)
            {
                ActivateEnemy(enemy);
                enemy.enemyObject.GetComponent<EnemyController>().Init(CalculateHP(enemy));
                OnInitEnemy?.Invoke();
                break;
            }
        }
    }

    int CalculateHP(Enemy enemy)
    {

        int currentRoom = GameplayController.instance.currentRoom;

        if (currentRoom == 1) return enemy.baseHP;

        return Mathf.RoundToInt(enemy.baseHP * Mathf.Pow((1 + incrementoHP), currentRoom - 1));
    }

    void ActivateEnemy(Enemy selectedEnemy)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.enemyObject.SetActive(false); // Desactiva todas las salas
        }

        selectedEnemy.enemyObject.SetActive(true); // Activa la sala elegida
        GameplayController.instance.ActivarAutoClick();
    }
}

public enum EnemyRare
{
    Basic,
    Common,
    Rare,
    Epic,
    Legendary,
    Mythical
}
