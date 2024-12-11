using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class ScreenUI : MonoBehaviour
    { 

 
    #region Private Fields
        [SerializeField]
        private DragonController dragonController;     
        [SerializeField]
        private DragonHealthController dragonHealth;
        [SerializeField]
        private TextMeshProUGUI playerNameText;
        [SerializeField]
        private TextMeshProUGUI SaveZoneMinimapTimeText;
        [SerializeField]
        private TextMeshProUGUI SaveZoneZoommapTimeText;
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
        if (!dragonController.photonView.IsMine)
        {
            gameObject.SetActive(false);
        }

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
        SaveZoneMinimapTimeText.text = SafeZoneSystem.Instance.timeFormatted;
        SaveZoneZoommapTimeText.text = SafeZoneSystem.Instance.timeFormatted;
        
         
       
        
    }
    #endregion
}
