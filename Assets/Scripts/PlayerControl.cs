using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // ���������� � �������������� Rigidbody. 
    // ����� ����������� �������� ������������ ��������� � �������������� ���������� playerSpeed;
    private Rigidbody characterRigidbodyComponent;
    public float playerSpeed = 0f;

    // Start is called before the first frame update

    // ���������� Rigidbody � ����������, ������� ������������ ��� ������������ ���������
    void Start()
    {
        characterRigidbodyComponent = GetComponent<Rigidbody>();
    }


    // � update ������� �����, ���������� �� �������� ���������

    // Update is called once per frame
    void Update()
    {
        GetMove();
    }


    // ����� GetMove() �������� �� �������� ���������
    // �� ���� ��������� ��������� position � ���������� Rigidbody2D.
    // ���������� playerSpeed �������� �� �������� ����������� � ������������
    // Time.deltaTime ��� ���������� ������������ �� �������� � ������ �������������������
    private void GetMove()
    {
        Vector3 playerMoveInput = new Vector3
            (Input.GetAxis("Horizontal")*playerSpeed, 0f, Input.GetAxis("Vertical")*playerSpeed);
        characterRigidbodyComponent.MovePosition(characterRigidbodyComponent.position + playerMoveInput*Time.deltaTime);
    }

    //���� ��� � ��������� �������� �� ������� �� ������ �����.
    //����� � ��� ���������������, �.�. �� �� ������������ � ������� ������� �����
    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<PlayerController>().enabled = false;
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        GetComponent<PlayerController>().enabled = true;
        StopCoroutine(LoadNextScene());
    }
    */
}
