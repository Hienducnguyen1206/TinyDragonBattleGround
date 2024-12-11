using DG.Tweening;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof(DragonCurrentStats))]
public class DragonHealthController : MonoBehaviour,IHurt
{
    public PhotonView photonView;
    [SerializeField] DragonCurrentStats currentStats;
    [SerializeField] Image healthBar;
    [SerializeField] Image staminaBar;


    public static Action ZeroStaminaAction;
   


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
        photonView = gameObject.GetComponent<PhotonView>();
        health = currentStats.currentmaxHealth;
        stamina = currentStats.currentmaxStamina;
        decreaseRate = 10f;
        increaseRate = currentStats.currentStaminaRegenPerSecond;
        healthBar.fillAmount = 1f;
        staminaBar.fillAmount = 1f;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            staminaBar.fillAmount = stamina / currentStats.currentmaxStamina;
        }
          
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
            .OnComplete(() =>
            {            
                ZeroStaminaAction.Invoke();
               
            });
    }
    private void IncreaseStamina()
    {
        staminaTween = DOTween.To(() => stamina, x => stamina = x, currentStats.currentmaxStamina, (currentStats.currentmaxStamina - stamina) / increaseRate)
            .SetEase(Ease.Linear);
    }
    private void RegenHealthPoint()
    {
        healthTween = DOTween.To(() => health, x => health = x, currentStats.currentmaxHealth, (currentStats.currentmaxHealth - health) / healthRegenPerSecond)
            .SetEase(Ease.Linear);           
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        if (health - damage <= 0)
        {
            health = 0;
            Debug.Log("PlayerDeath");
        }
        else
        {
            health -= damage;
        }
    }

}
