using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int BulletsInAmmo = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.AddAmmo(BulletsInAmmo); // Добавляем патроны
            Debug.Log("Игрок получил " + BulletsInAmmo);
            Destroy(gameObject);
        }
    }
}
