using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // Кого камера будет преследовать
    public float smoothSpeed = 5f; // Скорость сглаживания
    public Vector3 offset; // Смещение камеры относительно игрока

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); // Сохраняем Z камеры
    }
}
