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

    [SerializeField] GameObject rocket;
    [SerializeField] GameObject player;
    [SerializeField] GameObject secondCam;
    
    [SerializeField] float flyAwaySpeed = 0.01f;

    private CameraFollowScript camChange;
    private bool ifPlayerDestroyed = false;
    //����� OnCollisionEnter ��������� ����� ������ � ������� ������� ��������.
    //���� ������ ����� ��� "Enemy", �� ����������� ����� CollisionText � �������� TextClear.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rocket")
        {
            CollisionText();
            StartCoroutine(TextClear());

            if (Input.GetKey(KeyCode.E))
            {
                secondCam.GetComponent<Camera>().enabled = true;
                GameObject.Destroy(player);
                StartCoroutine(flyCoroutine());
                StartCoroutine(nextSceneCoroutine());
            }
        }
    }

    //����� CollisionText ��������� �������� �� ����� (� ������� TextMeshPro) � � ������� �����
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to fly on the Earth" ;
    }
    //�������� TextClear ����� ��� ����, ����� �������� ������� TextMeshPro ������ 2 ������� ����� ��������������
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(2f);
        text.text = "";
        StopCoroutine(TextClear());
    }

    IEnumerator nextSceneCoroutine()
    {
        PlayerControl playerControl = GetComponent<PlayerControl>();
        playerControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
        StopCoroutine(flyCoroutine());
    }
    IEnumerator flyCoroutine()
    {
        while (1>0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x, rocket.transform.position.y + flyAwaySpeed, rocket.transform.position.z);
        rocket.GetComponent<Rigidbody>().MovePosition(flyVector);
    }
}
