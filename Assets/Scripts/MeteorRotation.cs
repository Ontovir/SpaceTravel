using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRotation : MonoBehaviour
{
    [SerializeField] List<GameObject> meteors;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MeteorRotationMethod();
    }
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
    private void Movement()
    {
        float moveSpeed = 600f * Time.deltaTime;
        Vector3 moveVector = new Vector3(100f, 100f, 100f);
        GetComponent<Rigidbody2D>().transform.position = Vector3.MoveTowards(transform.position, moveVector, moveSpeed);
    }
}
