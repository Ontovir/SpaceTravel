using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject objectToFollow;
    private Vector3 deltaPos;
    
    // Start is called before the first frame update
    void Start()
    {
        deltaPos = transform.position - objectToFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + deltaPos;
    }
}
