using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;  // Плавность следования камеры
    public Vector3 offset;              // Смещение камеры относительно игрока

    private Transform player;           // Игрок, за которым следует камера

    void Start()
    {
        // Находим локального игрока по его PhotonView
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {
            // Получаем объект локального игрока
            player = PhotonNetwork.LocalPlayer.TagObject as Transform;

            if (player == null)
            {
                // Если трансформ игрока еще не назначен, то ищем объект по тегу "Player"
                player = GameObject.FindGameObjectWithTag("Player")?.transform;
            }
        }
        else
        {
            Debug.LogError("Не удается найти локального игрока!");
        }
    }

    void Update()
    {
        // Убедимся, что это локальный игрок, и если да, то камера будет следовать за ним
        if (player != null && PhotonView.Get(this).IsMine)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Позиционирование камеры с плавным переходом
        Vector3 desiredPosition = player.position + offset;  // Задаем желаемую позицию камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // Плавное следование
        transform.position = smoothedPosition;  // Устанавливаем новую позицию камеры
    }
}
