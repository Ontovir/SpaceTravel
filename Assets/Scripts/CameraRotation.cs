using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotationMethod();
    }

    private void CameraRotationMethod()
    {
        Vector3 getMousePosition = Input.mousePosition;
        Quaternion rotation = Quaternion.Euler(-getMousePosition.y * 0.05f, getMousePosition.x * 0.05f, 0f);
        transform.rotation = rotation;
    }
}
