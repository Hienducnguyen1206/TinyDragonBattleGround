using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCurrentStats : MonoBehaviour
{

    [SerializeField] DragonStats dragonStats;

    [Header("Attributes")]
    public float currentmaxHealth;
    public float currentmaxStamina;
    public float currentHealthRegenPerSecond;
    public float currentStaminaRegenPerSecond;

    [Header("Attributes")]
    public float currentAttackPower;
    public float currentWalkSpeed;
    public float currentRunSpeed;
    public float currentFlySpeed;


    [Header("Abilities")]
    public float currentCriticalChance;
    public float currentCriticalDamageMultiplier;
    void Awake()
    {

         currentmaxHealth = dragonStats.maxHealth;
         currentmaxStamina = dragonStats.maxStamina;
         currentHealthRegenPerSecond = dragonStats.healthRegenPerSecond;
         currentStaminaRegenPerSecond = dragonStats.staminaRegenPerSecond;
         currentAttackPower = dragonStats.attackPower;
         currentWalkSpeed = dragonStats.walkSpeed;
         currentRunSpeed = dragonStats.runSpeed;
         currentFlySpeed = dragonStats.flySpeed;
         currentCriticalChance = dragonStats.criticalChance;
         currentCriticalDamageMultiplier = dragonStats.criticalDamageMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
