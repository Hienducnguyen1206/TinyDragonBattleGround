using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class ScreenUI : MonoBehaviour
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
        private TextMeshProUGUI playerHealthText;
        [SerializeField]
        private Slider playerStrengSlider;
        [SerializeField]
        private Slider playerExpSlider;
        [SerializeField] 
        private TextMeshProUGUI playerExpText;
        [SerializeField] 
        private TextMeshProUGUI playerLevelText;
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

  
    public void SetDragonController(DragonController _dragonController) {
        if (_dragonController == null)
        {
            Debug.LogError("Missing dragonController target for PlayerUI.SetDragonController.", this);
            return;
        }
        
        dragonController = _dragonController;

        dragonController.SetJoystick(fixedJoystick);
       

       // RunButton.onClick.AddListener(dragonController.RunButtonPressed);
         FlyButton.onClick.AddListener(dragonController.FlyButtonPressed);
    }

    void Update()
    {
        
        
         
       
        
    }
    #endregion
}
