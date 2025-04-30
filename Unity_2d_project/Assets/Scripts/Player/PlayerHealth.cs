using UnityEngine;

using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Damage Visual Settings")]
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;

    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;

        // Если не привязан spriteRenderer в инспекторе, ищем в дочерних объектах
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

    public void TakeDamage(int damage)
    {
        //if (currentHealth <= 0) return; // Уже мертв

        currentHealth -= damage;
        Debug.Log("Player HP: " + currentHealth);

        if (spriteRenderer != null)
        {
            StartCoroutine(DamageEffect());
        }

        if (currentHealth <= 0)
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

    void Die()
    {
        Debug.Log("Player is dead!");
        // Здесь можно сделать: анимацию смерти, перезагрузку сцены и т.д.
        gameObject.SetActive(false);
    }
}
