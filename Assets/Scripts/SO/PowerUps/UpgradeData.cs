using UnityEngine;


public abstract class UpgradeData : ScriptableObject
{
    public Sprite sprite;
    public string description;
    public float variacion;
}

public enum PowerupType
{
    ClickDamage,
    AutoClickDamage,
    AutoClickSpeed,
    CritChance,
    CritDamage
}

public enum GlobalBonusType
{
    ExtraVitality,
    ExtraKey,
    CostReduction,
    ChestChance,
    RecoverVitality
}
