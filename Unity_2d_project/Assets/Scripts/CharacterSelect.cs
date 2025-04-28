using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // ����� ��� CustomProperties

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter(string characterName)
    {
        Debug.Log("������ �������� " + characterName);
        Hashtable playerProperties = new Hashtable();
        playerProperties["character"] = characterName;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }
}
