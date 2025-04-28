using UnityEngine;
using Photon.Pun;  // <-- �������

using System.Collections;

public class Enemy : MonoBehaviour
{
    public int enemyHp = 10;
    public int currentEnemyHP;
    public int damage = 20;  // ����, ������� ������� ����

    [Header("Damage Visual Settings")]
    public SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;
    private Color originalColor;

    private PhotonView view; // <-- �������

    public void Start()
    {
        currentEnemyHP = enemyHp;
        view = GetComponent<PhotonView>(); // <-- �������

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void Die()
    {
        if (view.IsMine) // <-- ������ �������� ������� ���������� ���
        {
            PhotonNetwork.Destroy(gameObject);
        }
        Debug.Log("����� ���� �����!");
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        currentEnemyHP -= damage;
        Debug.Log("Enemy HP: " + currentEnemyHP);

        if (spriteRenderer != null)
        {
            StartCoroutine(DamageEffect());
        }

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

    IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = originalColor;
    }
}
