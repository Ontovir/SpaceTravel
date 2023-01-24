using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    // ���������������� ��������� ��� ������ ������ ����������.
    Vector3 moveVector;
    CharacterController rocketControl;
    public float moveSpeed = 10f;

    // ���������� rocketControl ����������� ��������� CharacterController ������
    // Start is called before the first frame update
    void Start()
    {
        rocketControl = GetComponent<CharacterController>();
    }

    // � Update ���������� ����� RocketMove, ���������� �� �������� ������ � ������������
    // Update is called once per frame
    void Update()
    {
        RocketMove();
    }

    //����� RocketMove ������������ ��� ����������� ������ � ������������
    //�� �������� ����� �������� ������ � ������������ RocketRotation
    //�������� �������������� �� ���� ���������� character controller ����� ���������,
    //����������� �� ��� "X" � ������� moveVector.
    //�������� �������������� �� ���� Input.GetAxis("Vetrical"), ����������� �� Time.deltaTime (�����������)
    //� �� �������� ����������� ������ � ������������ 
    private void RocketMove()
    {
        RocketRotation();
        moveVector = new Vector3(Input.GetAxis("Vertical")*Time.deltaTime*moveSpeed, 0f, 0f);
        rocketControl.Move(moveVector);
        
    }

    //����� RocketRotation �������� �� ������� ������ � ������������ � ������� �����
    //������ getMousePosition �������� ���������� ������� ���� �� Input.mousePosition
    //�� ������� �� � Quaternion rotation, � ������ � ����� Quaternion.Euler
    //Quaternion rotation ������� �������� �� CharacterController, ������������ ������� ������ � ������� �����.
    private void RocketRotation()
    {
        Vector3 getMousePosition = Input.mousePosition;
        Quaternion rotation = Quaternion.Euler(0f, getMousePosition.x*0.01f, getMousePosition.y*0.01f);
        rocketControl.transform.rotation = rotation;
    }
}
