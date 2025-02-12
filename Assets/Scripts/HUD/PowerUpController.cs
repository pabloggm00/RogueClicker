using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    public static PowerUpController instance;

    private void Awake()
    {
        instance = this;
    }

    [Header("UI Setup")]
    // Prefab que contiene el script PowerUpPrefab
    public GameObject powerUpPrefab;
    

    // Referencia al UpgradeManager
    public UpgradeManager upgradeManager;

    /// <summary>
    /// Muestra 3 opciones de powerups de enemigo en la UI.
    /// </summary>
    public void ShowEnemyPowerUpOptions()
    {
        ClearContainer();
        List<PowerUp> options = upgradeManager.GetRandomEnemyPowerUps();

        foreach (PowerUp pu in options)
        {
            GameObject instance = Instantiate(powerUpPrefab, transform);
            instance.GetComponent<PowerUpPrefab>().Initialize(pu, upgradeManager, false);
        }
    }

    /// <summary>
    /// Muestra 3 opciones de bonificaciones globales en la UI.
    /// </summary>
    public void ShowGlobalBonusOptions()
    {
        ClearContainer();
        List<GlobalBonus> options = upgradeManager.GetRandomGlobalBonuses();

        foreach (GlobalBonus gb in options)
        {
            GameObject instance = Instantiate(powerUpPrefab, transform);
            instance.GetComponent<PowerUpPrefab>().Initialize(gb, upgradeManager, true);
        }
    }

    /// <summary>
    /// Limpia el contenedor de tarjetas.
    /// </summary>
    public void ClearContainer()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

   
}
