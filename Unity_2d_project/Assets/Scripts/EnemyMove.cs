using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float speed = 2f;
    public float wobbleAmount = 0.5f; // ��������� ����������� �� ����
    public float directionChangeInterval = 0.5f; // ��� ����� ����������� �����������

    private Transform player;
    private Vector2 currentDirection;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickDirection();
    }

    void Update()
    {
        transform.Translate(currentDirection * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            PickDirection();
            timer = 0f;
        }
    }

    void PickDirection()
    {
        if (player == null) return;

        Vector2 toPlayer = (player.position - transform.position).normalized;

        // ������� ��������� ��������
        Vector2 randomOffset = new Vector2(
            Random.Range(-wobbleAmount, wobbleAmount),
            Random.Range(-wobbleAmount, wobbleAmount)
        );

        currentDirection = (toPlayer + randomOffset).normalized;
    }
}
