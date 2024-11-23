using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider strengthSlider;
    [SerializeField] DragonCurrentStats currentStats;
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float maxStrength;
    [SerializeField] float currentStrength;

    private Coroutine decreaseStrengthCoroutine;  
    private Coroutine regenStrengthCoroutine;

    public static Action ZeroStrength;

    void Start()
    {   
        healthSlider = UIManager.Instance.healthSlider;
        strengthSlider = UIManager.Instance.strengthSlider;


        healthSlider.maxValue = currentStats.currentmaxHealth;
        healthSlider.value = currentStats.currentmaxHealth;

        strengthSlider.maxValue = currentStats.currentmaxStrength;
        strengthSlider.value = currentStats.currentmaxStrength;

        maxHealth = currentStats.currentmaxHealth;
        currentHealth = currentStats.currentmaxHealth;
        maxStrength = currentStats.currentmaxStrength;
        currentStrength = currentStats.currentmaxStrength;
    }

    void Update()
    {
       CheckStrength();
    }

    public void InitUIReferences()
    {

    }

    public void StartDecreaseStrength()
    {
        
        if (regenStrengthCoroutine != null)
        {
            StopCoroutine(regenStrengthCoroutine);
            regenStrengthCoroutine = null;  
        }

        
        if (decreaseStrengthCoroutine == null)
        {
            decreaseStrengthCoroutine = StartCoroutine(DecreaseStrength());
        }
    }

    public void StartIncreaseStrength()
    {
      
        if (decreaseStrengthCoroutine != null)
        {
            StopCoroutine(decreaseStrengthCoroutine);
            decreaseStrengthCoroutine = null; 
        }

        
        if (regenStrengthCoroutine == null)
        {
            regenStrengthCoroutine = StartCoroutine(StrengthRegen());
        }
    }

    IEnumerator DecreaseStrength()
    {
        while (currentStrength > 0)
        {
            currentStrength -= 0.15f;
            strengthSlider.value = currentStrength;
            yield return new WaitForSeconds(0.0125f);
        }



        decreaseStrengthCoroutine = null; 
    }

    IEnumerator StrengthRegen()
    {
        while (currentStrength < maxStrength)
        {
            currentStrength += 0.1f;
            strengthSlider.value = currentStrength;
            yield return new WaitForSeconds(0.0125f);
        }

        regenStrengthCoroutine = null; 
    }

    public void CheckStrength()
    {
        if (currentStrength <= 0)
        {
            ZeroStrength.Invoke();
        }
    }
}
