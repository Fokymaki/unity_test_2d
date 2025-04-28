using UnityEngine;
using Photon.Pun;

public class WeaponRotation : MonoBehaviourPunCallbacks
{
    void Update()
    {
        // ���������, ���� ��� ��� �����
        if (!photonView.IsMine)
            return;

        // �������� ������� ���������� ����
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;  // ��������, ��� z-���������� ������� 0, ��� ��� �� �������� � 2D

        // ����������� �� ������ � ����
        Vector2 direction = (mouseWorldPos - transform.position);

        // ������� ���� ��������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ������������ ������
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
