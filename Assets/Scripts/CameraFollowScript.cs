using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // —крипт, который используетс€ дл€ того, чтобы обеспечивать движение игровой камеры за игроком.
    // ќн используетс€ во второй сцене (Flying), вешаетс€ на игровую камеру
    // objectToFollow - игровой объект, за которым движетс€ камера (ракета)
    public GameObject objectToFollow;
    private Vector3 deltaPos;
    

    // ¬ Start высчитываетс€ вектор deltaPos, который показывает разницу между нахождением камеры и ракеты в пространстве
    // Start is called before the first frame update
    void Start()
    {
        deltaPos = transform.position - objectToFollow.transform.position;
    }

    // ¬ методе Update высчитываютс€ новые координаты камеры на основании позиции игрового объекта "ракета",
    // к которым прибавл€етс€ deltaPos
    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + deltaPos;
    }
}
