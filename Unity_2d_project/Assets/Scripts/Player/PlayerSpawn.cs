using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] playerPrefabs;  // Массив всех возможных персонажей
    public GameObject cameraPrefab;      // Префаб камеры

    public float minX, minY, maxX, maxY;

    void Start()
    {
        // Спавним персонажа и камеру для локального игрока
        string savedCharacterName = PlayerPrefs.GetString("SelectedCharacter", "");

        if (!string.IsNullOrEmpty(savedCharacterName))
        {
            SpawnCharacter(savedCharacterName);
        }
        else
        {
            Debug.LogWarning("Персонаж не выбран! Спавним стандартного.");
            SpawnCharacter(playerPrefabs[0].name); // Если не выбран - первый
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
            Debug.LogError("Не найден префаб с именем: " + characterName);
            prefabToSpawn = playerPrefabs[0]; // Резервный вариант
        }

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -1);
        GameObject playerObject = PhotonNetwork.Instantiate(prefabToSpawn.name, randomPosition, Quaternion.identity);

        // Создаем камеру для этого игрока
        CreatePlayerCamera(playerObject);
    }

    void CreatePlayerCamera(GameObject playerObject)
    {
        // Создаем камеру для игрока
        GameObject playerCamera = Instantiate(cameraPrefab);
        playerCamera.GetComponent<PlayerCamera>().player = playerObject.transform;  // Устанавливаем цель для камеры

        // Дополнительные настройки камеры, если нужно
        playerCamera.transform.SetParent(playerObject.transform);
    }
}
