using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCurrentStats : MonoBehaviour
{

    [SerializeField] DragonStats dragonStats;

    [Header("Attributes")]
    public float currentmaxHealth;
    public float currentmaxStrength;

    [Header("Attributes")]
    public float currentAttackPower;
    public float currentMovementSpeed;

    [Header("Abilities")]
    public float currentCriticalChance;
    public float currentCriticalDamageMultiplier;
    void Awake()
    {

        

         currentmaxHealth = dragonStats.maxHealth;
         currentmaxStrength = dragonStats.maxStrength;
         currentAttackPower = dragonStats.attackPower;
         currentMovementSpeed = dragonStats.movementSpeed;
         currentCriticalChance = dragonStats.criticalChance;
         currentCriticalDamageMultiplier = dragonStats.criticalDamageMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
