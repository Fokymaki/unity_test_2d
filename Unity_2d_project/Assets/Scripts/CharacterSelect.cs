using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Нужно для CustomProperties

public class CharacterSelect : MonoBehaviour
{
    public void SelectCharacter(string characterName)
    {
        Debug.Log("Выбран персонаж " + characterName);
        Hashtable playerProperties = new Hashtable();
        playerProperties["character"] = characterName;
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }
}
