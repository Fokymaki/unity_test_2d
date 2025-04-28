using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class PlayerSpawn : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public float minX, minY, maxX, maxY;

    void Start()
    {
        string selectedCharacterName = "";

        // Получаем имя персонажа из Custom Properties
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("character"))
        {
            selectedCharacterName = (string)PhotonNetwork.LocalPlayer.CustomProperties["character"];
        }
        else
        {
            Debug.LogWarning("Персонаж не выбран! Спавним стандартного.");
            selectedCharacterName = playerPrefabs[0].name;
        }

        SpawnCharacter(selectedCharacterName);
    }

    void SpawnCharacter(string characterName)
    {
        GameObject prefabToSpawn = null;

        foreach (GameObject prefab in playerPrefabs)
        {
            if (prefab.name == characterName)
            {
                prefabToSpawn = prefab;
                break;
            }
        }

        if (prefabToSpawn == null)
        {
            Debug.LogError("Не найден префаб с именем: " + characterName);
            prefabToSpawn = playerPrefabs[0]; // Резервный вариант
        }

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -1);
        PhotonNetwork.Instantiate(prefabToSpawn.name, randomPosition, Quaternion.identity);
    }
}
