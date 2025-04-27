using UnityEngine;


public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Пуля врага
    public float bulletSpeed = 5f;
    public float shootInterval = 2f; // Интервал стрельбы
    private float shootTimer;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        shootTimer = shootInterval;
    }

    void Update()
    {
        if (player == null)
            return;

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        // Повернуть пулю лицом к движению (если нужно)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
