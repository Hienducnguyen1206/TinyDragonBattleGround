using UnityEngine;

[CreateAssetMenu(fileName = "NewDragonStats", menuName = "Dragon/Dragon Stats")]
public class DragonStats: ScriptableObject
{
    [Header("Basic Stats")]
    public string dragonName;
    public float maxHealth;
    public float maxStamina;
    public float healthRegenPerSecond;
    public float staminaRegenPerSecond;

    [Header("Attributes")]
    public int attackPower;
    public float walkSpeed;
    public float runSpeed;
    public float flySpeed;

    [Header("Abilities")]
    public float criticalChance;
    public float criticalDamageMultiplier;
}
