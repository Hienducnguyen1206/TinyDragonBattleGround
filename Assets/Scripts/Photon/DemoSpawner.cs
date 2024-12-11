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

    void Start()
    {
        // Chỉ tạo đối tượng khi player sở hữu
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.LocalPlayer.IsLocal)
        {
            GameObject player = PhotonNetwork.Instantiate("DragonSD_13", new Vector3(0, 1, 2), Quaternion.identity);
            GameObject _uiGo = Instantiate(PlayerUIPrefab);

            // Gắn các tham chiếu UI chỉ cho player sở hữu
            MinimapCameraFollow.Instance.SendMessage("SetPlayerTransform", player.transform, SendMessageOptions.RequireReceiver);
            MinimapCameraFollow.Instance.SendMessage("SetZoomSlider", _uiGo.transform.GetChild(4).transform.GetComponentInChildren<Slider>(), SendMessageOptions.RequireReceiver);

            _uiGo.GetComponent<ScreenUI>().SetDragonController(player.GetComponent<DragonController>());
           

            // Tạo camera chỉ cho player sở hữu
            CinemachineVirtualCamera camera = Instantiate(virtualCamera);
            camera.Follow = player.transform;
        }
    }

    void Update()
    {
        // Có thể xử lý logic nếu cần
    }
}
