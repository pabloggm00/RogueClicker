using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Referencia al Player")]
    public PlayerController player;

    [Header("Enemy PowerUps (por enemigo derrotado)")]
    public List<PowerUp> enemyPowerUps = new List<PowerUp>();

    [Header("Global Bonuses (cada 10 salas)")]
    public List<GlobalBonus> globalBonuses = new List<GlobalBonus>();


    public List<PowerUp> GetRandomEnemyPowerUps(int count = 3)
    {
        List<PowerUp> options = new List<PowerUp>(enemyPowerUps);
        List<PowerUp> selected = new List<PowerUp>();

        for (int i = 0; i < count && options.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, options.Count);
            selected.Add(options[randomIndex]);
            options.RemoveAt(randomIndex);
        }
        return selected;
    }

    public List<GlobalBonus> GetRandomGlobalBonuses(int count = 3)
    {
        List<GlobalBonus> options = new List<GlobalBonus>(globalBonuses);
        List<GlobalBonus> selected = new List<GlobalBonus>();

        for (int i = 0; i < count && options.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, options.Count);
            selected.Add(options[randomIndex]);
            options.RemoveAt(randomIndex);
        }
        return selected;
    }


    public void ApplyEnemyUpgrade(PowerUp pu)
    {
        switch (pu.type)
        {
            case PowerupType.ClickDamage:
                // Se asume que AumentarDmg acepta un int
                player.AumentarDmg((int)pu.variacion);
                Debug.Log("Applied Enemy Upgrade: Click Damage +" + pu.variacion);
                break;
            case PowerupType.AutoClickDamage:
                player.AumentarAutoclickDmg((int)pu.variacion);
                Debug.Log("Applied Enemy Upgrade: Auto-Click Damage +" + pu.variacion);
                break;
            case PowerupType.AutoClickSpeed:
                // Para AutoClickSpeed, asumimos que "variacion" es el porcentaje de reducción (por ejemplo, 0.05 para 5%)
                float reductionAmount = player.intervalAutoclick * pu.variacion;
                player.DisminuirIntervalAutoclick(reductionAmount);
                Debug.Log("Applied Enemy Upgrade: Auto-Click Speed, interval reduced by " + reductionAmount);
                break;
            case PowerupType.CritChance:
                player.AumentarCriticalProb(pu.variacion);
                Debug.Log("Applied Enemy Upgrade: Crit Chance +" + (pu.variacion * 100) + "%");
                break;
            case PowerupType.CritDamage:
                player.AumentarCriticalDmg(pu.variacion);
                Debug.Log("Applied Enemy Upgrade: Crit Damage +" + pu.variacion);
                break;
        }
    }

    public void ApplyGlobalBonus(GlobalBonus gb)
    {
        // Usamos GameplayController.instance para aplicar los efectos globales.
        switch (gb.type)
        {
            case GlobalBonusType.ExtraVitality:  // Ahora se entenderá como "Extra Vitalidad"
                GameplayController.instance.AddExtraVitalidad((int)gb.variacion);
                Debug.Log("Applied Global Bonus: Extra Vitalidad +" + gb.variacion);
                break;
            case GlobalBonusType.ExtraKey:
                player.AumentarLlaves((int)gb.variacion);
                Debug.Log("Applied Global Bonus: Extra Key +" + gb.variacion);
                break;
            case GlobalBonusType.CostReduction:
                GameplayController.instance.ApplyCombatCostReduction(gb.variacion, 5); // Duración fija de 5 salas, por ejemplo
                Debug.Log("Applied Global Bonus: Combat Cost Reduction -" + (gb.variacion * 100) + "% for 5 rooms");
                break;
            case GlobalBonusType.ChestChance:
                GameplayController.instance.IncreaseChestChance(gb.variacion);
                Debug.Log("Applied Global Bonus: Chest Chance Increase +" + (gb.variacion * 100) + "%");
                break;
            case GlobalBonusType.RecoverVitality:
                // Ahora se entiende como Vitalidad Recovery, si lo deseas renombrar en el enum
                GameplayController.instance.SetVitalidadRecoveryBonus(gb.variacion, 5); // Ejemplo: recupera 5 vitalidad
                Debug.Log("Applied Global Bonus: Vitalidad Recovery " + (gb.variacion * 100) + "% chance for +5 vitalidad per enemy");
                break;
        }
    }
}
