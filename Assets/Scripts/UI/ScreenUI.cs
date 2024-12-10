using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class ScreenUI : MonoBehaviour
    { 

    public static ScreenUI instance;
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
        [SerializeField]
        private Button FollowPlayerButton;

    #endregion

    #region MonoBehaviour Callbacks

    #endregion

    #region Public Methods

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        FollowPlayerButton.onClick.AddListener(FollowPlayerOnMinimap);
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
         FireButton.onClick.AddListener(()=> { dragonController.photonView.RPC("Fire", RpcTarget.All);});

    }

    public void FollowPlayerOnMinimap()
    {
       
        if (MinimapCameraFollow.Instance.FollowPlayer != true)
        {
            MinimapCameraFollow.Instance.FollowPlayer = true;
        }
       
    }


    void Update()
    {
        
        
         
       
        
    }
    #endregion
}
