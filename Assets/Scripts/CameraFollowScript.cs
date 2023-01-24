using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // ������, ������� ������������ ��� ����, ����� ������������ �������� ������� ������ �� �������.
    // �� ������������ �� ������ ����� (Flying), �������� �� ������� ������
    // objectToFollow - ������� ������, �� ������� �������� ������ (������)
    public GameObject objectToFollow;
    private Vector3 deltaPos;
    

    // � Start ������������� ������ deltaPos, ������� ���������� ������� ����� ����������� ������ � ������ � ������������
    // Start is called before the first frame update
    void Start()
    {
        deltaPos = transform.position - objectToFollow.transform.position;
    }

    // � ������ Update ������������� ����� ���������� ������ �� ��������� ������� �������� ������� "������",
    // � ������� ������������ deltaPos
    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + deltaPos;
    }
}
