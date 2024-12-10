using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class DemoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerUIPrefab;

    

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
           
            GameObject player = PhotonNetwork.Instantiate("DragonSD_13", new Vector3(0,1,2), Quaternion.identity);
          //  Debug.Log("spawn player");
            GameObject _uiGo = Instantiate(PlayerUIPrefab);
            
            MinimapCameraFollow.Instance.SendMessage("SetPlayerTransform", player.transform, SendMessageOptions.RequireReceiver);
            MinimapCameraFollow.Instance.SendMessage("SetZoomSlider",_uiGo.transform.GetChild(4).transform.GetComponentInChildren<Slider>(),SendMessageOptions.RequireReceiver);
         //  _uiGo.SendMessage("SetDragonController", player.GetComponent<DragonController>(), SendMessageOptions.RequireReceiver);
             _uiGo.GetComponent<ScreenUI>().SetDragonController(player.GetComponent<DragonController>());
            SafeZoneSystem.Instance.SetTimeText(_uiGo.transform.GetChild(3).transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>());
            CinemachineVirtualCamera camera = Instantiate(virtualCamera);
            camera.Follow = player.transform;
            
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
