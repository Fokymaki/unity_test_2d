using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;  // ��������� ���������� ������
    public Vector3 offset;              // �������� ������ ������������ ������

    private Transform player;           // �����, �� ������� ������� ������

    void Start()
    {
        // ������� ���������� ������ �� ��� PhotonView
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {
            // �������� ������ ���������� ������
            player = PhotonNetwork.LocalPlayer.TagObject as Transform;

            if (player == null)
            {
                // ���� ��������� ������ ��� �� ��������, �� ���� ������ �� ���� "Player"
                player = GameObject.FindGameObjectWithTag("Player")?.transform;
            }
        }
        else
        {
            Debug.LogError("�� ������� ����� ���������� ������!");
        }
    }

    void Update()
    {
        // ��������, ��� ��� ��������� �����, � ���� ��, �� ������ ����� ��������� �� ���
        if (player != null && PhotonView.Get(this).IsMine)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // ���������������� ������ � ������� ���������
        Vector3 desiredPosition = player.position + offset;  // ������ �������� ������� ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // ������� ����������
        transform.position = smoothedPosition;  // ������������� ����� ������� ������
    }
}
