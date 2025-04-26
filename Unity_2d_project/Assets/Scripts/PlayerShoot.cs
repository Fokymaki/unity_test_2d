using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform gunTransform; // Сюда в инспекторе подставишь пушку

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Создаём пулю в позиции пушки
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);

        // Получаем Rigidbody
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Направление прямо по пушке
        Vector2 shootDirection = gunTransform.up; // В 2D вверх (ось Y) — это направление вперёд
        rb.linearVelocity = shootDirection * bulletSpeed;
    }
}
