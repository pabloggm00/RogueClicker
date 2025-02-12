using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public int penaltyDefeated;
    public PlayerController playerController;

    [HideInInspector]
    public EnemyController currentEnemy;
    [HideInInspector]
    public int currentRoom;


    // Variables globales para los efectos de bonus:
    // En este caso, la "moneda" es la vitalidad.
    // Puedes definirla aqu� o, alternativamente, gestionar la vitalidad en el PlayerController.
    // Por ejemplo, si decides que la vitalidad se almacena en el jugador, no es necesario duplicarla aqu�.
    // Para este ejemplo, usaremos el m�todo AumentarVitalidad del playerController.

    // Variables para otros bonus globales (coste de combate, aparici�n de cofres, recuperaci�n de vitalidad, etc.)
    public float combatCostReductionFactor = 0f;  // Porcentaje de reducci�n del coste de combate (0 = sin reducci�n)
    public int combatCostReductionDuration = 0;   // N�mero de salas que dura la reducci�n de coste
    public float chestChance = 0.1f;              // Probabilidad base de aparici�n de cofre (ej. 10%)
    public float vitalidadRecoveryChance = 0f;    // Probabilidad de recuperar vitalidad al derrotar enemigos
    public int vitalidadRecoveryAmount = 0;       // Cantidad de vitalidad a recuperar cuando se activa la recuperaci�n


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NextRoom();

        PlayerController.OnPlayerAutoClick += AutoDamageEnemy;
    }

    public void NextRoom()
    {
        currentRoom++;
        // Disminuir la duraci�n de la reducci�n del coste de combate
        if (combatCostReductionDuration > 0)
        {
            combatCostReductionDuration--;
            if (combatCostReductionDuration == 0)
            {
                combatCostReductionFactor = 0f; // Se reinicia el bonus
            }
        }
        RoomGenerator.instance.CreateRoom();

        if (currentRoom > 1)
        {
            BackgroundParticles.instance.UpdateParticleSettings(currentRoom);
        }

    }

    public void ActivarAutoClick()
    {
        playerController.StartAutoClick();
    }

    void DesactivarAutoClick()
    {
        playerController.StopAutoclick();
    }

    void AutoDamageEnemy(int dmg, bool isCritical, bool isAutoclick)
    {
        currentEnemy.TakeDamage(dmg, isCritical, isAutoclick);
    }

    public int GetKeys()
    {
        return playerController.keys;
    }

    public void EnemyDeath()
    {
        DesactivarAutoClick();
        RoomGenerator.instance.DisableAll();
        DamagePopUpGenerator.instance.DestroyAllNumber();
        ShowPowerUps();
    }

    public void ShowPowerUps()
    {



        if (currentRoom % 10 == 0)  // Si es una sala m�ltiplo de 10
        {
            // Muestra los Global Bonuses (cada 10 salas)
            PowerUpController.instance.ShowGlobalBonusOptions();
        }
        else
        {
            // Muestra los PowerUps normales
            PowerUpController.instance.ShowEnemyPowerUpOptions();
        }
    }


    // M�TODOS GLOBALES (BONUS)

    /// <summary>
    /// A�ade vitalidad extra al jugador.
    /// </summary>
    /// <param name="amount">Cantidad de vitalidad a a�adir.</param>
    public void AddExtraVitalidad(int amount)
    {
        playerController.AumentarVitalidad(amount);
        Debug.Log("Extra Vitalidad added: +" + amount + ". New vitalidad: " + playerController.vitalidad);
    }

    /// <summary>
    /// Aplica una reducci�n del coste de combate por un n�mero de salas.
    /// Por ejemplo, si el coste base es 5, una reducci�n del 10% lo har� 4.5.
    /// </summary>
    /// <param name="reduction">Porcentaje de reducci�n (ej. 0.10 para 10%).</param>
    /// <param name="duration">Duraci�n en n�mero de salas.</param>
    public void ApplyCombatCostReduction(float reduction, int duration)
    {
        combatCostReductionFactor = reduction;
        combatCostReductionDuration = duration;
        Debug.Log("Combat cost reduction applied: -" + (reduction * 100) + "% for " + duration + " rooms.");
    }

    /// <summary>
    /// Incrementa la probabilidad de que aparezca un cofre.
    /// </summary>
    /// <param name="amount">Valor a incrementar (por ejemplo, 0.05 para +5%).</param>
    public void IncreaseChestChance(float amount)
    {
        chestChance += amount;
        Debug.Log("Chest chance increased by " + (amount * 100) + "%. New chance: " + (chestChance * 100) + "%");
    }

    /// <summary>
    /// Establece la bonificaci�n de recuperaci�n de vitalidad.
    /// </summary>
    /// <param name="chance">Probabilidad de que se recupere vitalidad (ej. 0.10 para 10%).</param>
    /// <param name="amount">Cantidad de vitalidad a recuperar cuando se active.</param>
    public void SetVitalidadRecoveryBonus(float chance, int amount)
    {
        vitalidadRecoveryChance = chance;
        vitalidadRecoveryAmount = amount;
        Debug.Log("Vitalidad recovery bonus set: " + (chance * 100) + "% chance for +" + amount + " vitalidad per enemy.");
    }
}
