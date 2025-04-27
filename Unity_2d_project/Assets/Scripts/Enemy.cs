using UnityEngine;

using System.Collections;
public class Enemy : MonoBehaviour
{
    public int enemyHp = 10;
    public int currentEnemyHP;
    public int damage = 20;  // ”рон, который наносит враг

    [Header("Damage Visual Settings")]
    public SpriteRenderer spriteRenderer; // —юда прив€жешь спрайт из инспектора
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;
    private Color originalColor;
    public void Start()
    {
        currentEnemyHP = enemyHp;
    }
    public void Die()
    {
        Destroy(gameObject); // ”ничтожаем врага
        Debug.Log("»грок убил врага!");
    }
    public void TakeDamage(int damage)
    {
        currentEnemyHP -= damage;
        Debug.Log("Enemy HP: " + currentEnemyHP);

     /*   if (spriteRenderer != null)
        {
            StartCoroutine(DamageEffect());
        }*/

        if (currentEnemyHP <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
    /* IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = originalColor;
    }*/
}

