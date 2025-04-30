using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] playerPrefabs;  // ������ ���� ��������� ����������
    public GameObject cameraPrefab;      // ������ ������

    public float minX, minY, maxX, maxY;

    void Start()
    {
        // ������� ��������� � ������ ��� ���������� ������
        string savedCharacterName = PlayerPrefs.GetString("SelectedCharacter", "");

        if (!string.IsNullOrEmpty(savedCharacterName))
        {
            SpawnCharacter(savedCharacterName);
        }
        else
        {
            Debug.LogWarning("�������� �� ������! ������� ������������.");
            SpawnCharacter(playerPrefabs[0].name); // ���� �� ������ - ������
        }
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
            Debug.LogError("�� ������ ������ � ������: " + characterName);
            prefabToSpawn = playerPrefabs[0]; // ��������� �������
        }

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -1);
        GameObject playerObject = PhotonNetwork.Instantiate(prefabToSpawn.name, randomPosition, Quaternion.identity);

        // ������� ������ ��� ����� ������
        CreatePlayerCamera(playerObject);
    }

    void CreatePlayerCamera(GameObject playerObject)
    {
        // ������� ������ ��� ������
        GameObject playerCamera = Instantiate(cameraPrefab);
        playerCamera.GetComponent<PlayerCamera>().player = playerObject.transform;  // ������������� ���� ��� ������

        // �������������� ��������� ������, ���� �����
        playerCamera.transform.SetParent(playerObject.transform);
    }
}
