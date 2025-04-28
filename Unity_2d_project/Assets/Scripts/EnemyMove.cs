using UnityEngine;
using Photon.Pun;

public class EnemyMove : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 2f;
    private Vector3 networkPosition; // ���� ����� �������� ������� �� ������ �������
    private Quaternion networkRotation; // ���� ����� �������� ������� �� ������ �������

    private void Start()
    {
        networkPosition = transform.position;
        networkRotation = transform.rotation;
    }

    private void Update()
    {
        if (photonView.IsMine || PhotonNetwork.IsMasterClient)
        {
            Move();
        }
        else
        {
            // ������ ������������� �������
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    private void Move()
    {
        // ��� ���� ������ �������� �����
        transform.Translate(Vector2.left * speed * Time.deltaTime); // ��������, ������ �����
    }

    // ���� ����� ����� ��� ������������� �� ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �� ���������� ���� ������
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // �� �������� ������
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
