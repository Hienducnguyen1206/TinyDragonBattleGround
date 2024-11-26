using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class DemoPhotonNetWorking : MonoBehaviourPunCallbacks
{
    [SerializeField] Button CreateRoomBtn;
    [SerializeField] TMP_InputField RoomName;
    [SerializeField] TextMeshProUGUI Status;
    [SerializeField] Image connectIcon;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        CreateRoomBtn.onClick.AddListener(CreateRoom);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
        Status.text = "Loading ...";
        connectIcon.color = Color.yellow;
        Status.color = Color.yellow;
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Status.text = "Connected";
        connectIcon.color = Color.green;
        Status.color = Color.green; 
    }

    public void CreateRoom()
    {
        if (RoomName.text == "")
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(RoomName.text, roomOptions);
       
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Create Room Success");
     
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Create Room Faile" + message );
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("GameScene");
        Debug.Log("Join Room Success");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Join Room Fail" + message);
    }

}
