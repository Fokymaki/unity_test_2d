using UnityEngine;
using Photon.Pun;

public class WeaponRotation : MonoBehaviourPunCallbacks
{
    void Update()
    {
        // Проверяем, если это наш игрок
        if (!photonView.IsMine)
            return;

        // Получаем мировые координаты мыши
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;  // Убедимся, что z-координата остаётся 0, так как мы работаем в 2D

        // Направление от оружия к мыши
        Vector2 direction = (mouseWorldPos - transform.position);

        // Находим угол поворота
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Поворачиваем оружие
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
