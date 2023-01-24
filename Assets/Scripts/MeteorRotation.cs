using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRotation : MonoBehaviour
{
    //Список метеоритов, на которые будет действовать скрипт
    [SerializeField] List<GameObject> meteors;

    //В Update вызывается MeteorRotationMember
    //Метод обеспечивает вращение метеоров в случайном направлении
    // Update is called once per frame
    void Update()
    {
        MeteorRotationMethod();
    }

    //В методе MeteorRotationMethod используется функция foreach,
    //которая назначает случайный вектор направления вращения
    //каждому элементу из списка meteors.
    //С помощью метода Rotate задаётся вращение каждому item из списка
    private void MeteorRotationMethod()
    {
        foreach (var item in meteors)
        {
            float randX = Random.Range(0.01f, 0.2f);
            float randY = Random.Range(0.01f, 0.2f);
            float randZ = Random.Range(0.01f, 0.2f);
            item.transform.Rotate(new Vector3(randX, randY, randZ));
        }
    }
}
