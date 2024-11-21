using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragonHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider strengthSlider;
    [SerializeField] DragonStats dragonStats;


    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = dragonStats.maxHealth;
        healthSlider.value = dragonStats.maxHealth;

        strengthSlider.maxValue = dragonStats.maxStrength;
        strengthSlider.value = dragonStats.maxStrength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
