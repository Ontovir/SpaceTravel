using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollisionScript : MonoBehaviour
{
    //SerializeField ��� ������ TextMeshPro, ������� ����� ����������, ��� ��������� �������.
    //������������� ������� TextMeshPro ������ � Unity � ����� �������.
    [SerializeField] TextMeshProUGUI text;

    // ������� �������, ������� ������������ � �������. 
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject player;
    [SerializeField] GameObject secondCam;
    
    //�������� ����� ������ � ����� ����� � �������
    [SerializeField] float flyAwaySpeed = 0.01f;

    //������ ���������� ifCollisionHappened ������������ � ������ IfEKeyPressed
    private bool ifCollisionHappened = false;


    //����� OnCollisionEnter ��������� ����� ������ � ������� ������� ��������.
    //���� ������ ����� ��� "Rocket", �� ����������� ����� CollisionText � �������� TextClear.
    //������� ���������� ifCollisionHappened ������������� �������� true.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rocket")
        {
            ifCollisionHappened = true;
            CollisionText();
            StartCoroutine(TextClear());
        }
    }

    //� Update ���������� ����� IfEKeyPressed. �����������, ���������� �� ������� � � ��������� �� ������� ������ � �������.
    //��� ��������� ����� �����.
    private void Update()
    {
        IfEKeyPressed();
    }

    //����� IfEKeyPressed �������� �� �������� ������� ������� � � ������� �������� ������ � �������.
    //���� ����� �������� ������� �, �������� � �������� � �������, �����:
    //1. ���������� ������ ������, ������� ���������� ����� �����
    //2. ������������ ������� ������ Player, ���������� � ���� ������ ������
    //3. ����������� �������� FlyCoroutine - ���� ������ � �������
    //4. ����������� �������� NextSceneCoroutine - ������� �� ������ ������� �����
    private void IfEKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.E) && ifCollisionHappened)
        {
            secondCam.GetComponent<Camera>().enabled = true;
            GameObject.Destroy(player);
            StartCoroutine(NextSceneCoroutine());
            StartCoroutine(FlyCoroutine());
        }
    }

    //����� CollisionText ��������� �������� �� ����� (� ������� TextMeshPro) � � ������� �����
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to fly on the Earth";
    }
    //�������� TextClear ����� ��� ����, ����� �������� ������� TextMeshPro ������ ����� ����� ��������������
    //�����, ������ �������� ����������� �������� ������� ���������� ifCollsionHappened = false, ��� ����, �����
    //�������� ��������� ������ IfEKeyPressed, ����� ����� �� ��������� � �������� � �������.
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(1f);
        text.text = "";
        ifCollisionHappened = false;
        StopCoroutine(TextClear());
    }

    //�������� NextSceneCoroutine ����������� ����� ������ ����� ������.
    //� ���� �������� ���������� ��������� ��������:
    //1. ������ � ����������� ������ PlayerControl � ������� �������, ������� ���������� ������ ������ (PlayerCollisionScript)
    //2. ���������� �������� 5 ������ - ��� ����� ����� ��� ����, ����� �������� ����� � ������ ������.
    //3. ����������� SceneManager.LoadScene, ���������� ������� �� ����� ������������ �������
    //4. ��������������� ��������, ���������� �� ����� � ������� ������ � �������.
    //5. ��������������� �������� �������� �� ��������� �����.
    IEnumerator NextSceneCoroutine()
    {
        PlayerControl playerControl = GetComponent<PlayerControl>();
        playerControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
        StopCoroutine(FlyCoroutine());
        StopCoroutine(NextSceneCoroutine());
    }

    //�������� FlyCoroutine ������ ����� ����� ������ 
    //� �������� ������������ ����������� ���� while, ������� �������� ����� FlyOnEarth() ������ 0.033 �������
    //��������� �����, ������ ���������� ����������� � ������ � �������.
    IEnumerator FlyCoroutine()
    {
        while (1>0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }

    //����� FlyOnEarth �������� �� �������� ������ � ������ � ����������� �������.
    //� ������ ������������ ����� ���������� Rigidbody, ������������ �������� ������� rocket.
    //���������� ����� MovePosition, � ������� ��������� ���������� � ����������� �� ������� flyVector
    //flyVector ���������� ���������� �������� ������� rocket � ������������. �� ��� "Y" � ������ ��������� ���������� 
    //� �������� ����� ������ � ����������� �������
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x, rocket.transform.position.y + flyAwaySpeed*Time.deltaTime, rocket.transform.position.z);
        rocket.GetComponent<Rigidbody>().MovePosition(flyVector);
    }
}
