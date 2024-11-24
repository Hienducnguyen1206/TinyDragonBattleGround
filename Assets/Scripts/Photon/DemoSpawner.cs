using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DemoSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
       
            GameObject player = PhotonNetwork.Instantiate("DragonSD_13", Vector3.zero, Quaternion.identity);


            GameObject _uiGo = Instantiate(PlayerUIPrefab);
       
            _uiGo.SendMessage("SetDragonHealth", player.GetComponent<DragonHealth>(), SendMessageOptions.RequireReceiver);
            _uiGo.SendMessage("SetDragonController", player.GetComponent<DragonController>(), SendMessageOptions.RequireReceiver);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
