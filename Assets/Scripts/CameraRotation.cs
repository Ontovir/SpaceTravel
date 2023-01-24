using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    //Update вызывает CameraRotationMethod для поворота камеры
    // Update is called once per frame
    void Update()
    {
        CameraRotationMethod();
    }

    //Метод CameraRotationMethod отвечает за поворот ракеты в пространстве с помощью мышки
    //Вектор getMousePosition забирает координаты курсора мыши из Input.mousePosition
    //Он передаёт их в Quaternion rotation, а именно в метод Quaternion.Euler
    //Quaternion rotation передаёт вращение на игровой объект, к которому прикреплён скрипт (камера),
    //обеспечивает поворот камеры с помощью мышки.
    private void CameraRotationMethod()
    {
        Vector3 getMousePosition = Input.mousePosition;
        Quaternion rotation = Quaternion.Euler(-getMousePosition.y * 0.05f, getMousePosition.x * 0.05f, 0f);
        transform.rotation = rotation;
    }
}
