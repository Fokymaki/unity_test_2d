using UnityEngine;
using Photon.Pun;

public class EnemyMove : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 2f;
    public float detectionRadius = 10f; // Радиус видимости
    public float swayAmount = 0.5f; // Амплитуда покачивания
    public float swaySpeed = 2f; // Скорость покачивания
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    private Transform player;
    private bool isPlayerInRange = false;
    private float swayOffset = 0f; // Смещение для покачивания

    private Rigidbody2D rb; // Добавляем ссылку на Rigidbody2D

    private void Start()
    {
        networkPosition = transform.position;
        networkRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine || PhotonNetwork.IsMasterClient)
        {
            FindNearestPlayer(); // Ищем ближайшего игрока
            if (isPlayerInRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                IdleSway();
            }
        }
        else
        {
            // Плавно интерполируем позицию
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime);
        }
    }

    private void FindNearestPlayer()
    {
        // Получаем всех игроков на сцене
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closestPlayer = null;
        float closestDistance = float.MaxValue;

        // Ищем ближайшего игрока
        foreach (GameObject playerObj in players)
        {
            float distance = Vector3.Distance(transform.position, playerObj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = playerObj.transform;
            }
        }

        if (closestPlayer != null && closestDistance <= detectionRadius)
        {
            player = closestPlayer;
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Плавно покачиваем NPC при движении
        swayOffset = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Направление движения NPC к игроку
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Двигаемся к игроку с использованием Rigidbody2D
        Vector2 movement = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized * speed;
        rb.linearVelocity = new Vector2(movement.x, movement.y);

        // Поворот NPC в сторону игрока
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        // Покачивание NPC
        transform.position += transform.right * swayOffset;
    }

    private void IdleSway()
    {
        // Покачивание на месте
        swayOffset = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position += transform.right * swayOffset;
    }

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
