using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
