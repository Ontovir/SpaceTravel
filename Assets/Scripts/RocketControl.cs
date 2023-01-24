using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    // Инициализируются требуемые для работы метода переменные.
    Vector3 moveVector;
    CharacterController rocketControl;
    public float moveSpeed = 10f;

    // Переменной rocketControl назначается компонент CharacterController ракеты
    // Start is called before the first frame update
    void Start()
    {
        rocketControl = GetComponent<CharacterController>();
    }

    // В Update вызывается метод RocketMove, отвечающий за движение ракеты в пространстве
    // Update is called once per frame
    void Update()
    {
        RocketMove();
    }

    //Метод RocketMove используется для перемещения ракеты в пространстве
    //Он вызывает метод поворота ракеты в пространстве RocketRotation
    //Движение обеспечивается за счёт назначения character controller новых координат,
    //фиксируемых по оси "X" в векторе moveVector.
    //Движение обеспечивается за счёт Input.GetAxis("Vetrical"), умножаемого на Time.deltaTime (сглаживание)
    //и на скорость перемещения ракеты в пространстве 
    private void RocketMove()
    {
        RocketRotation();
        moveVector = new Vector3(Input.GetAxis("Vertical")*Time.deltaTime*moveSpeed, 0f, 0f);
        rocketControl.Move(moveVector);
        
    }

    //Метод RocketRotation отвечает за поворот ракеты в пространстве с помощью мышки
    //Вектор getMousePosition забирает координаты курсора мыши из Input.mousePosition
    //Он передаёт их в Quaternion rotation, а именно в метод Quaternion.Euler
    //Quaternion rotation передаёт вращение на CharacterController, обеспечивает поворот ракеты с помощью мышки.
    private void RocketRotation()
    {
        Vector3 getMousePosition = Input.mousePosition;
        Quaternion rotation = Quaternion.Euler(0f, getMousePosition.x*0.01f, getMousePosition.y*0.01f);
        rocketControl.transform.rotation = rotation;
    }
}
