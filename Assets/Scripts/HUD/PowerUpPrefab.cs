using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPrefab : MonoBehaviour
{
    public Image image;
    public TMP_Text description;
    public TMP_Text textNumber;
    public Button buttonPowerUp;


    // Bandera que indica si esta tarjeta es de bonificación global (cada 10 salas)
    public bool isGlobal;

    // Referencia al UpgradeData (puede ser PowerUp o GlobalBonus)
    private UpgradeData upgradeData;

    // Referencia al UpgradeManager para aplicar el upgrade
    private UpgradeManager upgradeManager;

    /// <summary>
    /// Inicializa la tarjeta con los datos del upgrade y la referencia al manager.
    /// </summary>
    /// <param name="data">Los datos del upgrade (PowerUp o GlobalBonus).</param>
    /// <param name="manager">Referencia al UpgradeManager.</param>
    /// <param name="global">Indica si es un bonus global.</param>
    public void Initialize(UpgradeData data, UpgradeManager manager, bool global = false)
    {
        upgradeData = data;
        upgradeManager = manager;
        isGlobal = global;

        image.sprite = data.sprite;
        description.text = data.description;
        textNumber.text = "+" + data.variacion.ToString();

        buttonPowerUp.onClick.AddListener(() => OnClick());
    }

    /// <summary>
    /// Método que se invoca al hacer clic en la tarjeta.
    /// Se asigna al evento OnClick del componente Button.
    /// </summary>
    public void OnClick()
    {
        if (upgradeManager == null || upgradeData == null)
            return;

        // Si es global, se aplica la bonificación global; de lo contrario, el powerup de enemigo.
        if (isGlobal)
        {
            // Se debe hacer un cast a GlobalBonus
            GlobalBonus gb = upgradeData as GlobalBonus;
            if (gb != null)
            {
                upgradeManager.ApplyGlobalBonus(gb);
            }
        }
        else
        {
            PowerUp pu = upgradeData as PowerUp;
            if (pu != null)
            {
                upgradeManager.ApplyEnemyUpgrade(pu);
            }
        }

        // Después de aplicar, se puede destruir la tarjeta
        //Destroy(gameObject);

        PowerUpController.instance.ClearContainer();
        GameplayController.instance.NextRoom();
    }

}
