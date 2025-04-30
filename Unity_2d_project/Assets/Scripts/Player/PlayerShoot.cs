using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб шарика
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position);
        direction.Normalize();

        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Получаем Rigidbody
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Правильно поворачиваем пулю (если пуля "носом вверх")
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f; // Поворот Rigidbody, а не transform!

        // Выставляем скорость
        rb.linearVelocity = direction * bulletSpeed;

        
       
    }
}
