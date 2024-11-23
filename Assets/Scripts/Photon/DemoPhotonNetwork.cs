using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class DemoPhotonNetwork : MonoBehaviourPunCallbacks
{
    [SerializeField] Button CreateRoomBtn;
    [SerializeField] Button JoinRoomBtn;
    [SerializeField] TMP_InputField RoomName;
    [SerializeField] TextMeshProUGUI StatusText;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override 


}
