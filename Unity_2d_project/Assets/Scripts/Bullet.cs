using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f); // Удалить объект через 5 секунд
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Проверяем, враг ли это
        {
            collision.GetComponent<Enemy>()?.Die(); // Убиваем врага
            Destroy(gameObject); // Удаляем пулю
        }
    }
}
