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

    [SerializeField] GameObject rocket;
    [SerializeField] GameObject MainCamera;

    [SerializeField] float flyAwaySpeed = 50f;
    private bool ifCollisionHappened = false;
    //����� OnCollisionEnter ��������� ����� ������ � ������� ������� ��������.
    //���� ������ ����� ��� "Enemy", �� ����������� ����� CollisionText � �������� TextClear.
    private void OnTriggerEnter()
    {
        //if (collision.collider.tag == "Rocket")
        Debug.Log("col");
        {
            CollisionText();
            StartCoroutine(TextClear());
            ifCollisionHappened = true;
        }
    }
    private void Update()
    {
        IfEKeyPressed();
    }

    private void IfEKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.E) && ifCollisionHappened)
        {
            MainCamera.GetComponent<CameraFollowScript>().enabled = false;
            StartCoroutine(flyCoroutine());
            StartCoroutine(nextSceneCoroutine());
        }
    }

    //����� CollisionText ��������� �������� �� ����� (� ������� TextMeshPro) � � ������� �����
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to land the Earth";
    }
    //�������� TextClear ����� ��� ����, ����� �������� ������� TextMeshPro ������ 2 ������� ����� ��������������
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(5f);
        text.text = "";
        ifCollisionHappened = false;
        StopCoroutine(TextClear());
    }

    IEnumerator nextSceneCoroutine()
    {
        RocketControl rocketControl = GetComponent<RocketControl>();
        rocketControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
        StopCoroutine(flyCoroutine());
    }
    IEnumerator flyCoroutine()
    {
        while (1 > 0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x + flyAwaySpeed, rocket.transform.position.y, rocket.transform.position.z);
        rocket.GetComponent<CharacterController>().Move(flyVector*Time.deltaTime);
    }
}
