using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // ���������� � �������������� Rigidbody. 
    // ����� ����������� �������� ������������ ��������� � �������������� ���������� playerSpeed;
    private Rigidbody playerRigidbodyComponent;
    public float playerSpeed = 1f;

    // Start is called before the first frame update

    // ���������� Rigidbody � ����������, ������� ������������ ��� ������������ ���������
    void Start()
    {
        playerRigidbodyComponent = GetComponent<Rigidbody>();
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
            (Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerRigidbodyComponent.MovePosition(playerRigidbodyComponent.position + (playerMoveInput * playerSpeed * Time.deltaTime));
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
