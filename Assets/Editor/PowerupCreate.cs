using System.IO;
using UnityEditor;
using UnityEngine;

public class PowerupCreate: EditorWindow
{
    private string powerUpFolder = "Assets/PowerUps/Cada enemigo";
    private string globalBonusFolder = "Assets/PowerUps/Cada 10 Rondas";

    [MenuItem("Tools/Generate PowerUps")]
    public static void ShowWindow()
    {
        GetWindow<PowerupCreate>("PowerUp Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("PowerUp Generator", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate PowerUps"))
        {
            GeneratePowerUps();
        }
    }

    private void GeneratePowerUps()
    {
        if (!Directory.Exists(powerUpFolder))
        {
            Directory.CreateDirectory(powerUpFolder);
        }

        if (!Directory.Exists(globalBonusFolder))
        {
            Directory.CreateDirectory(globalBonusFolder);
        }

        CreateEnemyPowerUps();
        CreateGlobalBonuses();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("PowerUps generated successfully!");
    }

    private void CreateEnemyPowerUps()
    {
        CreatePowerUp(PowerupType.ClickDamage, "Click Damage", 5f);
        CreatePowerUp(PowerupType.AutoClickDamage, "AutoClick Damage", 3f);
        CreatePowerUp(PowerupType.AutoClickSpeed, "AutoClick Speed", 0.1f);
        CreatePowerUp(PowerupType.CritChance, "Critical Chance", 0.05f);
        CreatePowerUp(PowerupType.CritDamage, "Critical Damage", 10f);
    }

    private void CreateGlobalBonuses()
    {
        CreatePowerUpGlobal(GlobalBonusType.ExtraVitality, "Extra Vitality", 20f);
        CreatePowerUpGlobal(GlobalBonusType.ExtraKey, "Extra Key", 1f);
        CreatePowerUpGlobal(GlobalBonusType.CostReduction, "Reduced Combat Cost", 10f);
        CreatePowerUpGlobal(GlobalBonusType.ChestChance, "Increased Chest Chance", 5f);
        CreatePowerUpGlobal(GlobalBonusType.RecoverVitality, "Recover Vitality on Kill", 5f);
    }

    private void CreatePowerUp(PowerupType type, string description, float value)
    {
        PowerUp powerUp = ScriptableObject.CreateInstance<PowerUp>();
        powerUp.type = type;
        powerUp.description = description;
        powerUp.variacion = value;
        powerUp.sprite = null; // Dejar vacío para que el usuario lo asigne manualmente

        string assetPath = $"{powerUpFolder}/{type.ToString()}.asset";
        AssetDatabase.CreateAsset(powerUp, assetPath);
    }

    private void CreatePowerUpGlobal(GlobalBonusType type, string description, float value)
    {
        GlobalBonus powerUp = ScriptableObject.CreateInstance<GlobalBonus>();
        powerUp.type = type;
        powerUp.description = description;
        powerUp.variacion = value;
        powerUp.sprite = null; // Dejar vacío para que el usuario lo asigne manualmente

        string assetPath = $"{globalBonusFolder}/{type.ToString()}.asset";
        AssetDatabase.CreateAsset(powerUp, assetPath);
    }
}
