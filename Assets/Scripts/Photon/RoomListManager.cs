using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomListManager : MonoBehaviourPunCallbacks {

    [SerializeField] Button roomButtonPrefab;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i] != null && roomList[i].PlayerCount >= 1 && roomList[i].IsVisible && roomList[i].IsOpen)
            {
                Button roombtn = Instantiate(roomButtonPrefab,this.transform);
                roombtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = roomList[i].Name;
                roombtn.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text=roomList[i].PlayerCount.ToString() + '/' + roomList[i].MaxPlayers.ToString();
               
            }
        }
    }

    public void JoinRoomByName(string roomName) { 
        PhotonNetwork.JoinRoom(roomName);
    }

}
