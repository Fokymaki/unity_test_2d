using UnityEngine;


public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject); // Уничтожаем врага
        Debug.Log("Игрок убил врага!");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.Die(); // Убиваем игрока
        }
    }
}

