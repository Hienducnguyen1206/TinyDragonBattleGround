using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class DemoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerUIPrefab;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        
            GameObject player = PhotonNetwork.Instantiate("DragonSD_13", Vector3.zero, Quaternion.identity);
            Debug.Log("spawn player");
            GameObject _uiGo = Instantiate(PlayerUIPrefab);
           
           _uiGo.SendMessage("SetDragonHealth", player.GetComponent<DragonHealth>(), SendMessageOptions.RequireReceiver);
           _uiGo.SendMessage("SetDragonController", player.GetComponent<DragonController>(), SendMessageOptions.RequireReceiver);
            CinemachineVirtualCamera camera = Instantiate(virtualCamera);
            camera.Follow = player.transform;
            
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
