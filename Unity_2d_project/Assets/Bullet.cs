using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f); // ������� ������ ����� 5 ������
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // ���������, ���� �� ���
        {
            collision.GetComponent<Enemy>()?.Die(); // ������� �����
            Destroy(gameObject); // ������� ����
        }
    }
}
