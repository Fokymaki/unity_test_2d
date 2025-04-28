using UnityEngine;
using Photon.Pun;

public class EnemyMove : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 2f;
    private Vector3 networkPosition; // Сюда будем получать позицию от других игроков
    private Quaternion networkRotation; // Сюда будем получать поворот от других игроков

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
            // Плавно интерполируем позицию
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    private void Move()
    {
        // Тут твоя логика движения врага
        transform.Translate(Vector2.left * speed * Time.deltaTime); // Например, просто влево
    }

    // Этот метод нужен для синхронизации по сети
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Мы отправляем свои данные
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Мы получаем данные
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
