using UnityEngine;

[CreateAssetMenu(fileName = "NewDragonStats", menuName = "Dragon/Dragon Stats")]
public class DragonStats: ScriptableObject
{
    [Header("Basic Stats")]
    public string dragonName;
    public float maxHealth;
    public float maxStrength;

    [Header("Attributes")]
    public float attackPower;
    public float movementSpeed;

    [Header("Abilities")]
    public float criticalChance;
    public float criticalDamageMultiplier;
}
