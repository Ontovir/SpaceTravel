using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollisionScript : MonoBehaviour
{
    //SerializeField для текста TextMeshPro, который будет показывать, что произошёл коллижн.
    //Привязывается элемент TextMeshPro холста в Unity к этому скрипту.
    [SerializeField] TextMeshProUGUI text;

    // Игровые объекты, которые используются в скрипте. 
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject player;
    [SerializeField] GameObject secondCam;
    
    //Скорость взлёта ракеты в сцене взлёта с платены
    [SerializeField] float flyAwaySpeed = 0.01f;

    //Булева переменная ifCollisionHappened используется в методе IfEKeyPressed
    private bool ifCollisionHappened = false;


    //Метод OnCollisionEnter запускает показ текста с помощью булевой проверки.
    //Если объект имеет тэг "Rocket", то запускается метод CollisionText и корутина TextClear.
    //Булевой переменной ifCollisionHappened присваивается значение true.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Rocket")
        {
            ifCollisionHappened = true;
            CollisionText();
            StartCoroutine(TextClear());
        }
    }

    //В Update вызывается метод IfEKeyPressed. Проверяется, нажимается ли клавиша Е и произошёл ли коллижн игрока с ракетой.
    //Для активации сцены взлёта.
    private void Update()
    {
        IfEKeyPressed();
    }

    //Метод IfEKeyPressed отвечает за проверку нажатия клавиши Е и статуса коллижна игрока с ракетой.
    //Если игрок нажимает клавишу Е, находясь в коллижне с ракетой, тогда:
    //1. Включается вторая камера, которая показывает сцену взлёта
    //2. Уничтожается игровой объект Player, содержащий в себе первую камеру
    //3. Запускается корутина FlyCoroutine - взлёт ракеты с планеты
    //4. Запускается корутина NextSceneCoroutine - переход на вторую игровую сцену
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

    //Метод CollisionText позволяет выводить на экран (в элемент TextMeshPro) и в консоль текст
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to fly on the Earth";
    }
    //Корутина TextClear нужна для того, чтобы очистить элемент TextMeshPro спустя время после взаимодействия
    //Также, данная корутина присваивает значение булевой переменной ifCollsionHappened = false, для того, чтобы
    //избежать активации метода IfEKeyPressed, когда игрок не находится в коллижне с ракетой.
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(1f);
        text.text = "";
        ifCollisionHappened = false;
        StopCoroutine(TextClear());
    }

    //Корутина NextSceneCoroutine запускается после начала взлёта ракеты.
    //В этой корутине происходят следующие действия:
    //1. Ищется и отключается скрипт PlayerControl в игровом объекте, который использует данный скрипт (PlayerCollisionScript)
    //2. Происходит ожидание 5 секунд - это время нужно для того, чтобы показать сцену с взлётом ракеты.
    //3. Запускается SceneManager.LoadScene, происходит переход на сцену космического перелёта
    //4. Останавливается корутина, отвечающая за сцену с вылетом ракеты с планеты.
    //5. Останавливается корутина перехода на следующую сцену.
    IEnumerator NextSceneCoroutine()
    {
        PlayerControl playerControl = GetComponent<PlayerControl>();
        playerControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
        StopCoroutine(FlyCoroutine());
        StopCoroutine(NextSceneCoroutine());
    }

    //Корутина FlyCoroutine создаёт сцену взлёта ракеты 
    //В корутине используется бесконечный цикл while, который вызывает метод FlyOnEarth() каждые 0.033 секунды
    //Благодаря этому, ракета неприрывно поднимается в воздух с планеты.
    IEnumerator FlyCoroutine()
    {
        while (1>0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }

    //Метод FlyOnEarth отвечает за поднятие ракеты в воздух с поверхности планеты.
    //В методе используется поиск компонента Rigidbody, присвоенного игровому объекту rocket.
    //Вызывается метод MovePosition, в который передаётся информация о координатах из вектора flyVector
    //flyVector использует координаты игрового объекта rocket в пространстве. По оси "Y" в вектор передаётся информация 
    //о скорости взлёта ракеты с поверхности планеты
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x, rocket.transform.position.y + flyAwaySpeed*Time.deltaTime, rocket.transform.position.z);
        rocket.GetComponent<Rigidbody>().MovePosition(flyVector);
    }
}
