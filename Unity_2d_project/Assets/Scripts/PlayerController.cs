using UnityEngine;


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
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Создаём пульку
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Двигаем её в сторону мышки
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }
}
