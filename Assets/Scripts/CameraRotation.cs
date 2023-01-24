using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //Update �������� CameraRotationMethod ��� �������� ������
    // Update is called once per frame
    void Update()
    {
        CameraRotationMethod();
    }

    //����� CameraRotationMethod �������� �� ������� ������ � ������������ � ������� �����
    //������ getMousePosition �������� ���������� ������� ���� �� Input.mousePosition
    //�� ������� �� � Quaternion rotation, � ������ � ����� Quaternion.Euler
    //Quaternion rotation ������� �������� �� ������� ������, � �������� ��������� ������ (������),
    //������������ ������� ������ � ������� �����.
    private void CameraRotationMethod()
    {
        Vector3 getMousePosition = Input.mousePosition;
        Quaternion rotation = Quaternion.Euler(-getMousePosition.y * 0.05f, getMousePosition.x * 0.05f, 0f);
        transform.rotation = rotation;
    }
}
