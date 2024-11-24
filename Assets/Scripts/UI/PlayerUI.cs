using UnityEngine;
using UnityEngine.UI;


    public class PlayerUI : MonoBehaviour
    {
    #region Private Fields
        [SerializeField]
        private DragonController dragonController;     
        [SerializeField]
        private DragonHealth dragonHealth;
        [SerializeField]
        private Text playerNameText;
        [SerializeField]
        private Slider playerHealthSlider;
        [SerializeField]
        private Slider playerStrengSlider;
        [SerializeField]
        private Slider playerExpSlider;
        [SerializeField]
        private FixedJoystick fixedJoystick;
        [SerializeField]
        private Button FireButton;
        [SerializeField]
        private Button RunButton;
        [SerializeField]
        private Button FlyButton;

    #endregion

    #region MonoBehaviour Callbacks

    #endregion

    #region Public Methods

    public void SetDragonHealth(DragonHealth _dragonHealth) { 
        if(_dragonHealth == null)
        {           
                Debug.LogError("Missing dragonHealth target for PlayerUI.SetDragonHealth.", this);
                return;            
        }

        Debug.Log("hehe");
        dragonHealth = _dragonHealth;
        playerHealthSlider.maxValue = dragonHealth.maxHealth;
        playerHealthSlider.value = dragonHealth.maxHealth;
        playerStrengSlider.maxValue = dragonHealth.maxHealth;
        playerStrengSlider.value = dragonHealth.maxHealth;
    }

    public void SetDragonController(DragonController _dragonController) {
        if (_dragonController == null)
        {
            Debug.LogError("Missing dragonController target for PlayerUI.SetDragonController.", this);
            return;
        }
        
        dragonController = _dragonController;

        dragonController.SetJoystick(fixedJoystick);
       

        RunButton.onClick.AddListener(dragonController.RunButtonPressed);
        FlyButton.onClick.AddListener(dragonController.FlyButtonPressed);
    }

    void Update()
    {
        
        
         
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = dragonHealth.currentHealth;
        }
        if (playerStrengSlider != null)
        {
            playerStrengSlider.value = dragonHealth.currentStrength;
        }

        if (dragonHealth == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
    }
    #endregion
}
