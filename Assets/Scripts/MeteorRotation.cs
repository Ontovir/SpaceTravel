using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRotation : MonoBehaviour
{
    //������ ����������, �� ������� ����� ����������� ������
    [SerializeField] List<GameObject> meteors;

    //� Update ���������� MeteorRotationMember
    //����� ������������ �������� �������� � ��������� �����������
    // Update is called once per frame
    void Update()
    {
        MeteorRotationMethod();
    }

    //� ������ MeteorRotationMethod ������������ ������� foreach,
    //������� ��������� ��������� ������ ����������� ��������
    //������� �������� �� ������ meteors.
    //� ������� ������ Rotate ������� �������� ������� item �� ������
    private void MeteorRotationMethod()
    {
        foreach (var item in meteors)
        {
            float randX = Random.Range(0.01f, 0.2f);
            float randY = Random.Range(0.01f, 0.2f);
            float randZ = Random.Range(0.01f, 0.2f);
            item.transform.Rotate(new Vector3(randX, randY, randZ));
        }
    }
}
