using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 5;
    public int bulletType = 0; //0 Это игрока, 1 врагов
    void Start()
    {
        Destroy(gameObject, 5f); // Удалить объект через 5 секунд
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy") && bulletType == 0) // Проверяем, враг ли это
        {
            collision.GetComponent<Enemy>()?.TakeDamage(bulletDamage); // Убиваем врага
                                                                       // Удаляем пулю
            Destroy(gameObject);
        }

       else if (collision.CompareTag("Player") && bulletType == 1) // Проверяем, игрок ли это
       {
            collision.GetComponent<PlayerController>()?.TakeDamage(bulletDamage); // Наносим урон игроку

            Destroy(gameObject);
       }
       else if (collision.CompareTag("Object"))
       {
            Destroy(gameObject);
       }
    }
}
