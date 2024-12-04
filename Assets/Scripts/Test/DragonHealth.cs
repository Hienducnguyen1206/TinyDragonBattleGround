using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DragonHealth : MonoBehaviour
{

    [SerializeField] DragonCurrentStats currentStats;
    [SerializeField] Image healthBar;
    [SerializeField] Image staminaBar;

  





    public Action RunZeroStamina;
    public Action FlyZeroStamina;


    [SerializeField] private float maxHealth;  
    [SerializeField] private float maxStamina;
    [SerializeField] private float decreaseRate; 
    [SerializeField] private float increaseRate;
    [SerializeField] private float healthRegenPerSecond;

    private float stamina;
    private float health;


    [SerializeField] private bool isDecreasing = false;

    private Tween staminaTween;
    private Tween healthTween;

    void Start()
    {   
       
        health = currentStats.currentmaxHealth;
        stamina = currentStats.currentmaxStamina;
        decreaseRate = 10f;
        increaseRate = currentStats.currentStaminaRegenPerSecond;
        healthBar.fillAmount = 1f;
        staminaBar.fillAmount = 1f;
    }

    void Update()
    {
        staminaBar.fillAmount = stamina / currentStats.currentmaxStamina;
        
        
    }

    public void ToggleStaminaChange()
    {

        staminaTween?.Kill();

        if (isDecreasing)
        {

            isDecreasing = false;
            IncreaseStamina();
        }
        else
        {

            isDecreasing = true;
            DecreaseStamina();
        }
    }

    private void DecreaseStamina()
    {

        staminaTween = DOTween.To(() => stamina, x => stamina = x, 0, stamina / decreaseRate)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
              //  Debug.Log("Stamina Decreasing: " + stamina);
            })
            .OnComplete(() =>
            {

              //  Debug.Log("Stamina depleted. Switching to increase.");
               
                RunZeroStamina.Invoke();
               
            });
    }
    private void IncreaseStamina()
    {

        staminaTween = DOTween.To(() => stamina, x => stamina = x, currentStats.currentmaxStamina, (currentStats.currentmaxStamina - stamina) / increaseRate)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
               // Debug.Log("Stamina Increasing: " + stamina);
            })
            .OnComplete(() =>
            {
               // Debug.Log("Stamina fully restored.");
            });
    }
    private void RegenHealthPoint()
    {
        healthTween = DOTween.To(() => health, x => health = x, currentStats.currentmaxHealth,(currentStats.currentmaxHealth - health) / healthRegenPerSecond)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                Debug.Log($"Health point: {health:0.00}");
            }).OnComplete(() =>
            {
                Debug.Log("Health is full");
            });
            
    }

}
