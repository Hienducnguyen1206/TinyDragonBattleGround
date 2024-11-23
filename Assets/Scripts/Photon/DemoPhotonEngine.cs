using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;


public class DemoPhotonEngine : MonoBehaviourPunCallbacks {
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI textState;
    [SerializeField] Button CreateRoomButton;
    [SerializeField] Button JoinRoomButton;
    [SerializeField] TMP_InputField RoomName;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        CreateRoomButton.onClick.AddListener(CreateRoom);
        JoinRoomButton.onClick.AddListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        textState.text = "Loading...";
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        textState.text = "Connected";
    }
    
    public void CreateRoom()
    {   
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(RoomName.text,roomOptions);

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Create room success");
        PhotonNetwork.LoadLevel("PlayScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Create room failed" + message);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);        
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join room success");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Join room fail" + message);
    }
}
