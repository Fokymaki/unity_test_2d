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

        // ���� �� �������� spriteRenderer � ����������, ���� � �������� ��������
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
            Debug.LogWarning("SpriteRenderer �� ������ � ������!");
        }
    }

    public void TakeDamage(int damage)
    {
        //if (currentHealth <= 0) return; // ��� �����

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
        // ����� ����� �������: �������� ������, ������������ ����� � �.�.
        gameObject.SetActive(false);
    }
}
