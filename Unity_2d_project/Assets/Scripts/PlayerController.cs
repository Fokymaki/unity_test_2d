using UnityEngine;

using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 5.0f;
    public float currentSpeed = 5.0f;
    public float dashSpeed = 300f;
    public float dashTime = 0.1f;
    private Rigidbody2D rb;
    private Vector2 input;
    public int playerAmmo = 10;
    public GameObject bulletPrefab; // Префаб шарика
    public float bulletSpeed = 10f;
    private bool canDash = true;
    private bool dashing = false;
    public int basePlayerHP = 100;
    private int currentPlayerHP;

    [Header("Damage Visual Settings")]
    public SpriteRenderer spriteRenderer; // Сюда привяжешь спрайт из инспектора
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;
    private Color originalColor;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPlayerHP = basePlayerHP;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer не найден у игрока!");
        }
    }
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetMouseButtonDown(0) && (playerAmmo > 0)) // Левая кнопка мыши
        {
            Shoot();
            playerAmmo--;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Debug.Log("Кувырок");
            Dash();
            dashTime = 0.1f;
        }
        if (dashing)
        {
            dashTime -= Time.deltaTime;
            if (dashTime < 0)
            {
                dashing = false;
                canDash = true;
                currentSpeed = baseSpeed;
              
            }
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * currentSpeed * Time.fixedDeltaTime);
    }
    public void Die()
    {
        Debug.Log("Игрок умер!");
        Destroy(gameObject); // Удаляем игрока
    }
    public void AddAmmo(int NewAmmo)
    {
        playerAmmo += NewAmmo;
    }
    void Dash()
    {
        currentSpeed = dashSpeed;
        dashing = true;

    }
    void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position);
        direction.Normalize();

        // Создаем пулю
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Поворачиваем пулю (если носик смотрит вверх)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle); // <-- ВАЖНО: минус 90 градусов

        // Двигаем пулю
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }
    public void TakeDamage(int damage)
    {
        currentPlayerHP -= damage;
        Debug.Log("Player HP: " + currentPlayerHP);

        if (spriteRenderer != null)
        {
            StartCoroutine(DamageEffect());
        }

        if (currentPlayerHP <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = originalColor;
    }
}
