using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    //Объекты, используемые в скрипте
    [SerializeField] GameObject player;
    [SerializeField] Camera landingCamera;

    //Переменные, используемые в скрипте
    float flyAwaySpeed = 60f;
    Rigidbody rocketRB;
    private bool isRocketLanded = false;


    // Start is called before the first frame update
    //В методе Start происходит поиск компонента ракеты Rigidbody.
    //Запускается корутина landingCoroutine, отвечающая за посадку ракеты на землю. 
    //Можно было бы осуществить посадку, просто назначив ракете гравитацию. Она бы упала и всё. Но мне
    //такой способ не понравился.
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        StartCoroutine(LandingCoroutine());
    }

    //Корутина LandingCoroutine работает, пока булева переменная isRocketLanded == false.
    //Эта корутина каждые 0.033 секунды вызывает метод LandingMethod, отвечающий за посадку ракеты.
    //Когда переменная isRocketLanded становится true, тогда отключается камера, показывающая посадку
    //и происходит Instantiate игрового объекта player. Включается опция компонента
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

    //Метод OnCollisionEnter срабатывает при соприкосновении с землёй.
    //Он назначает переменной flyAwaySpeed 0f, и меняет isRocketLanded на true, останавливает корутину LandingCoroutine
    private void OnCollisionEnter(Collision collision)
    {
        flyAwaySpeed = 0f;
        isRocketLanded = true;
        StopCoroutine(LandingCoroutine());
    }

    //LandingMethod отвечает за посадку ракеты на землю.
    //Движение происходит по оси Y. В метод MovePosition компонента ракеты Rigidbody передаются координаты вектора
    //flyVector, который с назначенной скоростью опускает ракету на землю. 
    private void LandingMethod()
    {
        float x = flyAwaySpeed * Time.deltaTime;
        Vector3 flyVector = new Vector3(rocketRB.transform.position.x, rocketRB.position.y - x, rocketRB.position.z);
        rocketRB.MovePosition(flyVector);
    }

}
