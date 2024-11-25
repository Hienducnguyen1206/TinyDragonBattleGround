using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnJoinRoom : MonoBehaviourPunCallbacks
{
     [SerializeField] TextMeshProUGUI roomName;
    void Start()
    {   
        if (roomName.text != "" && roomName != null)
        {
            transform.GetComponent<Button>().onClick.AddListener(() => PhotonNetwork.JoinRoom(roomName.text));
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
