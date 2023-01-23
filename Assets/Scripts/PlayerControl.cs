using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // ”правление с использованием Rigidbody. 
    // ћожно настраивать скорость передвижени€ персонажа с использованием переменной playerSpeed;
    private Rigidbody characterRigidbodyComponent;
    public float playerSpeed = 0f;

    // Start is called before the first frame update

    // ѕрисваиваю Rigidbody к переменной, котора€ используетс€ при передвижении персонажа
    void Start()
    {
        characterRigidbodyComponent = GetComponent<Rigidbody>();
    }


    // ¬ update вызываю метод, отвечающий за движение персонажа

    // Update is called once per frame
    void Update()
    {
        GetMove();
    }


    // ћетод GetMove() отвечает за контроль персонажа
    // за счЄт изменени€ параметра position у компонента Rigidbody2D.
    // ѕеременна€ playerSpeed отвечает за скорость перемещени€ в пространстве
    // Time.deltaTime дл€ унификации передвижени€ на системах с разной производительностью
    private void GetMove()
    {
        Vector3 playerMoveInput = new Vector3
            (Input.GetAxis("Horizontal")*playerSpeed, 0f, Input.GetAxis("Vertical")*playerSpeed);
        characterRigidbodyComponent.MovePosition(characterRigidbodyComponent.position + playerMoveInput*Time.deltaTime);
    }

    //Ётот код с корутиной отвечает за переход на другую сцену.
    //«десь € его закомментировал, т.к. он не используетс€ в текущей игровой сцене
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
