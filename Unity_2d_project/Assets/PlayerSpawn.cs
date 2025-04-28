using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public float minX, minY, maxX, maxY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY),-1);
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }

}
