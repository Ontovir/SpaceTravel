using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RocketCollisionScript : MonoBehaviour
{
    //SerializeField ��� ������ TextMeshPro, ������� ����� ����������, ��� ��������� �������.
    //������������� ������� TextMeshPro ������ � Unity � ����� �������.
    [SerializeField] TextMeshProUGUI text;

    // ������� �������, ������� ������������ � �������. 
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject MainCamera;

    //�������� ����� ������ � ����� ����� � �������
    [SerializeField] float flyAwaySpeed = 50f;

    //������ ���������� ifCollisionHappened ������������ � ������ IfEKeyPressed
    private bool ifCollisionHappened = false;

    //����� OnTriggerEnter ��������� ����� ������ � ������� ������� ��������.
    //������� ���������� ifCollisionHappened ������������� �������� true.
    private void OnTriggerEnter()
    {
            CollisionText();
            StartCoroutine(TextClear());
            ifCollisionHappened = true;
    }

    //� Update ���������� ����� IfEKeyPressed. �����������, ���������� �� ������� � � ��������� �� ������� ������ � �������� ������ �����.
    //��� ��������� ����� �������� ������ � �����.
    private void Update()
    {
        IfEKeyPressed();
    }

    //����� IfEKeyPressed �������� �� �������� ������� ������� � � ������� �������� ������ � �������� ������ �����.
    //���� ����� �������� ������� �, �������� � �������� � �������� ������ �����, �����:
    //1. ����������� ������ CameraFollowScript, ������� �������� �� ����������� ������ �� �������. ������ ������� �� �����
    //2. ����������� �������� FlyCoroutine - ������ ����� � �����
    //3. ����������� �������� NextSceneCoroutine - ������� �� ������ ������� ����� - �������� �� �����
    private void IfEKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.E) && ifCollisionHappened)
        {
            MainCamera.GetComponent<CameraFollowScript>().enabled = false;
            StartCoroutine(FlyCoroutine());
            StartCoroutine(NextSceneCoroutine());
        }
    }

    //����� CollisionText ��������� �������� �� ����� (� ������� TextMeshPro) � � ������� �����
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to land the Earth";
    }

    //�������� TextClear ����� ��� ����, ����� �������� ������� TextMeshPro ������ ����� ����� ��������������
    //�����, ������ �������� ����������� �������� ������� ���������� ifCollsionHappened = false, ��� ����, �����
    //�������� ��������� ������ IfEKeyPressed, ����� ����� �� ��������� � �������� � �������.
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(5f);
        text.text = "";
        ifCollisionHappened = false;
        StopCoroutine(TextClear());
    }

    //�������� NextSceneCoroutine ����������� ����� �������� ������ �� �����.
    //� ���� �������� ���������� ��������� ��������:
    //1. ������ � ����������� ������ RocketControl � ������� �������, ������� ���������� ������ ������ (RocketCollisionScript)
    //2. ���������� �������� 5 ������ - ��� ����� ����� ��� ����, ����� �������� ����� � �������� ������ �� �����.
    //3. ����������� SceneManager.LoadScene, ���������� ������� �� ����� ����������� ������ �� �����.
    //4. ��������������� ��������, ���������� �� ����� � ������� ������ � �������.
    //5. ��������������� �������� �������� �� ��������� �����.
    IEnumerator NextSceneCoroutine()
    {
        RocketControl rocketControl = GetComponent<RocketControl>();
        rocketControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
        StopCoroutine(FlyCoroutine());
        StopCoroutine(NextSceneCoroutine());
    }

    //�������� FlyCoroutine ������ ����� �������� ������ �� �����. 
    //� �������� ������������ ����������� ���� while, ������� �������� ����� FlyOnEarth() ������ 0.033 �������
    //��������� �����, ������ ���������� ������� �� �����.
    IEnumerator FlyCoroutine()
    {
        while (1 > 0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }

    //����� FlyOnEarth �������� �� �������� ������ �� �����.
    //� ������ ������������ ����� ���������� CharacterController, ������������ �������� ������� rocket.
    //���������� ����� Move, � ������� ��������� ���������� � ����������� �� ������� flyVector
    //flyVector ���������� ���������� �������� ������� rocket � ������������. �� ��� "X" � ������ ��������� ���������� 
    //� �������� ����� ������ � ����������� �������
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x + flyAwaySpeed, rocket.transform.position.y, rocket.transform.position.z);
        rocket.GetComponent<CharacterController>().Move(flyVector*Time.deltaTime);
    }
}
