using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RocketCollisionScript : MonoBehaviour
{
    //SerializeField для текста TextMeshPro, который будет показывать, что произошёл коллижн.
    //Привязывается элемент TextMeshPro холста в Unity к этому скрипту.
    [SerializeField] TextMeshProUGUI text;

    // Игровые объекты, которые используются в скрипте. 
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject MainCamera;

    //Скорость взлёта ракеты в сцене взлёта с платены
    [SerializeField] float flyAwaySpeed = 50f;

    //Булева переменная ifCollisionHappened используется в методе IfEKeyPressed
    private bool ifCollisionHappened = false;

    //Метод OnTriggerEnter запускает показ текста с помощью булевой проверки.
    //Булевой переменной ifCollisionHappened присваивается значение true.
    private void OnTriggerEnter()
    {
            CollisionText();
            StartCoroutine(TextClear());
            ifCollisionHappened = true;
    }

    //В Update вызывается метод IfEKeyPressed. Проверяется, нажимается ли клавиша Е и произошёл ли коллижн ракеты с областью вокруг земли.
    //Для активации сцены улетания ракеты к земле.
    private void Update()
    {
        IfEKeyPressed();
    }

    //Метод IfEKeyPressed отвечает за проверку нажатия клавиши Е и статуса коллижна ракеты с областью вокруг земли.
    //Если игрок нажимает клавишу Е, находясь в коллижне с областью вокруг земли, тогда:
    //1. Отключается скрипт CameraFollowScript, который отвечает за перемещение камеры за ракетой. Камера остаётся на месте
    //2. Запускается корутина FlyCoroutine - ракета летит к земле
    //3. Запускается корутина NextSceneCoroutine - переход на третью игровую сцену - действия на земле
    private void IfEKeyPressed()
    {
        if (Input.GetKeyDown(KeyCode.E) && ifCollisionHappened)
        {
            MainCamera.GetComponent<CameraFollowScript>().enabled = false;
            StartCoroutine(FlyCoroutine());
            StartCoroutine(NextSceneCoroutine());
        }
    }

    //Метод CollisionText позволяет выводить на экран (в элемент TextMeshPro) и в консоль текст
    private void CollisionText()
    {
        Debug.Log("Collision happened");
        text.text = "Press <<E>> to land the Earth";
    }

    //Корутина TextClear нужна для того, чтобы очистить элемент TextMeshPro спустя время после взаимодействия
    //Также, данная корутина присваивает значение булевой переменной ifCollsionHappened = false, для того, чтобы
    //избежать активации метода IfEKeyPressed, когда игрок не находится в коллижне с ракетой.
    IEnumerator TextClear()
    {
        yield return new WaitForSeconds(5f);
        text.text = "";
        ifCollisionHappened = false;
        StopCoroutine(TextClear());
    }

    //Корутина NextSceneCoroutine запускается после улетания ракеты на землю.
    //В этой корутине происходят следующие действия:
    //1. Ищется и отключается скрипт RocketControl в игровом объекте, который использует данный скрипт (RocketCollisionScript)
    //2. Происходит ожидание 5 секунд - это время нужно для того, чтобы показать сцену с улетания ракеты на землю.
    //3. Запускается SceneManager.LoadScene, происходит переход на сцену приземления ракеты на землю.
    //4. Останавливается корутина, отвечающая за сцену с вылетом ракеты с планеты.
    //5. Останавливается корутина перехода на следующую сцену.
    IEnumerator NextSceneCoroutine()
    {
        RocketControl rocketControl = GetComponent<RocketControl>();
        rocketControl.enabled = false;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
        StopCoroutine(FlyCoroutine());
        StopCoroutine(NextSceneCoroutine());
    }

    //Корутина FlyCoroutine создаёт сцену улетания ракеты на землю. 
    //В корутине используется бесконечный цикл while, который вызывает метод FlyOnEarth() каждые 0.033 секунды
    //Благодаря этому, ракета неприрывно улетает на землю.
    IEnumerator FlyCoroutine()
    {
        while (1 > 0)
        {
            FlyOnEarth();
            yield return new WaitForSeconds(0.033f);
        }
    }

    //Метод FlyOnEarth отвечает за улетание ракеты на землю.
    //В методе используется поиск компонента CharacterController, присвоенного игровому объекту rocket.
    //Вызывается метод Move, в который передаётся информация о координатах из вектора flyVector
    //flyVector использует координаты игрового объекта rocket в пространстве. По оси "X" в вектор передаётся информация 
    //о скорости взлёта ракеты с поверхности планеты
    private void FlyOnEarth()
    {
        Vector3 flyVector = new Vector3(rocket.transform.position.x + flyAwaySpeed, rocket.transform.position.y, rocket.transform.position.z);
        rocket.GetComponent<CharacterController>().Move(flyVector*Time.deltaTime);
    }
}
