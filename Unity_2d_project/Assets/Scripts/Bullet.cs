using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 5;
    public int bulletType = 0; // 0 - ������, 1 - ������

    void Start()
    {
        DestroyBullet(5f); // ������� ������ ����� 5 ������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && bulletType == 0)
        {
            PhotonView enemyView = collision.GetComponent<PhotonView>();
            Debug.Log("�����!");
            if (enemyView != null)
            {
                enemyView.RPC("TakeDamage", RpcTarget.All, bulletDamage);
            }
            DestroyBullet();
        }
        else if (collision.CompareTag("Player") && bulletType == 1)
        {
            PhotonView playerView = collision.GetComponent<PhotonView>();
            if (playerView != null)
            {
                playerView.RPC("TakeDamage", RpcTarget.All, bulletDamage);
            }
            DestroyBullet();
        }
        else if (collision.CompareTag("Object"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet(float delay = 0f)
    {
        if (delay > 0f)
        {
            Destroy(gameObject, delay);
        }
        else
        {
            if (GetComponent<PhotonView>() != null && GetComponent<PhotonView>().IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
