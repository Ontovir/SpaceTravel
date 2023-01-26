using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    //�������, ������������ � �������
    [SerializeField] GameObject player;
    [SerializeField] Camera landingCamera;

    //����������, ������������ � �������
    float flyAwaySpeed = 60f;
    Rigidbody rocketRB;
    private bool isRocketLanded = false;


    // Start is called before the first frame update
    //� ������ Start ���������� ����� ���������� ������ Rigidbody.
    //����������� �������� landingCoroutine, ���������� �� ������� ������ �� �����. 
    //����� ���� �� ����������� �������, ������ �������� ������ ����������. ��� �� ����� � ��. �� ���
    //����� ������ �� ����������.
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        StartCoroutine(LandingCoroutine());
    }

    //�������� LandingCoroutine ��������, ���� ������ ���������� isRocketLanded == false.
    //��� �������� ������ 0.033 ������� �������� ����� LandingMethod, ���������� �� ������� ������.
    //����� ���������� isRocketLanded ���������� true, ����� ����������� ������, ������������ �������
    //� ���������� Instantiate �������� ������� player. ���������� ����� ����������
    //Rigidbody useGravity.
    IEnumerator LandingCoroutine()
    {
        while (!isRocketLanded)
        {
            yield return new WaitForSeconds(0.033f);
            LandingMethod();
            if (isRocketLanded)
            {
                yield return new WaitForSeconds(1f);
                landingCamera.enabled = false;
                rocketRB.useGravity = true;
                Instantiate(player);
            }
        }
    }

    //����� OnCollisionEnter ����������� ��� ��������������� � �����.
    //�� ��������� ���������� flyAwaySpeed 0f, � ������ isRocketLanded �� true, ������������� �������� LandingCoroutine
    private void OnCollisionEnter(Collision collision)
    {
        flyAwaySpeed = 0f;
        isRocketLanded = true;
        StopCoroutine(LandingCoroutine());
    }

    //LandingMethod �������� �� ������� ������ �� �����.
    //�������� ���������� �� ��� Y. � ����� MovePosition ���������� ������ Rigidbody ���������� ���������� �������
    //flyVector, ������� � ����������� ��������� �������� ������ �� �����. 
    private void LandingMethod()
    {
        float x = flyAwaySpeed * Time.deltaTime;
        Vector3 flyVector = new Vector3(rocketRB.transform.position.x, rocketRB.position.y - x, rocketRB.position.z);
        rocketRB.MovePosition(flyVector);
    }

}
