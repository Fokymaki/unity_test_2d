using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 5;
    public int bulletType = 0; //0 ��� ������, 1 ������
    void Start()
    {
        Destroy(gameObject, 5f); // ������� ������ ����� 5 ������
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Enemy") && bulletType == 0) // ���������, ���� �� ���
        {
            collision.GetComponent<Enemy>()?.TakeDamage(bulletDamage); // ������� �����
                                                                       // ������� ����
            Destroy(gameObject);
        }

       else if (collision.CompareTag("Player") && bulletType == 1) // ���������, ����� �� ���
       {
            collision.GetComponent<PlayerController>()?.TakeDamage(bulletDamage); // ������� ���� ������

            Destroy(gameObject);
       }
       else if (collision.CompareTag("Object"))
       {
            Destroy(gameObject);
       }
    }
}
