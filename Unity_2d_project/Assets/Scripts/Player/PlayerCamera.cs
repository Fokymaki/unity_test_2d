using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float fixedZ = -10f;  // <--- Новая переменная для фиксации Z-позиции

    public Transform player;
    public PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView != null && !photonView.IsMine)
        {
            gameObject.SetActive(false);  // Отключаем камеру у чужих игроков
        }
    }

    public void SetPlayer(Transform target)
    {
        player = target;
    }

    void LateUpdate()
    {
        if (player != null && photonView.IsMine)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = fixedZ;  // <-- Фиксируем Z на нужное значение
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
