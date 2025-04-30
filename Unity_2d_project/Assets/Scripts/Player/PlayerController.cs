using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Характеристики персонажа")]
    public int baseStrength = 1;
    public int baseStamina = 1;
    public int baseAgility = 1;
    public int baseIntelligence = 1;
    private int currentStrength, currentStamina, currentAgility, currentIntelligence;
    private float baseSpeed = 5.0f;
    private int basePlayerHP = 100;
    private float currentSpeed = 5.0f;
    private float dashSpeed = 300f;
    private float dashTime = 0.1f;
    private Rigidbody2D rb;
    private Vector2 input;
    [Header("Инвентарь персонажа")]
    public int defence = 10;
    public int playerAmmo = 10;
    public GameObject[] weapon;
    public GameObject bulletPrefab; // Префаб шарика
    private float bulletSpeed = 10f;
    private bool canDash = true;
    private bool dashing = false;

    public LayerMask enemyLayer;
    public float attackRange = 5f;
    public Transform attackPoint;
    private int currentPlayerHP;

    [Header("Стрельба")]
    public SpriteRenderer spriteRenderer; // Сюда привяжешь спрайт из инспектора
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;
    private Color originalColor;

    PhotonView view;


    private void Start()
    {
       // rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        CharacterBaseStats();

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
        if(view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 moveAmmpount = moveInput.normalized * currentSpeed * Time.deltaTime;
            transform.position += (Vector3)moveAmmpount;
            //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Input.GetMouseButtonDown(0) && (playerAmmo > 0)) // Левая кнопка мыши
            {
                Shoot();
                playerAmmo--;
            }
            if (Input.GetMouseButtonDown(1)) // Левая кнопка мыши
            {
                MeleeAtack();
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
    public void MeleeAtack()
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayer); 
        foreach(Collider2D enemy in hitEnemys)
        {
            Debug.Log("Ударил врага " + enemy);
            enemy.GetComponent<Enemy>().TakeDamage(20);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange); // Gizmoz почитать
    }
    [PunRPC]
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
    public void CharacterBaseStats()
    {
        
        basePlayerHP =  1000 * baseStamina;
        baseSpeed = 1 * baseAgility;

        currentStrength = baseStrength;
        currentAgility = baseAgility;
        currentIntelligence = baseIntelligence;
        currentStamina = baseStamina;
        currentAgility = baseAgility;
        currentSpeed = baseSpeed;
        currentPlayerHP = basePlayerHP;
    }
}
