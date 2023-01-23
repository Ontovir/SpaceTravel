using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Camera landingCamera;
    float flyAwaySpeed = 60f;
    Rigidbody rocketRB;
    private bool isRocketLanded = false;
    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        StartCoroutine(landingCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator landingCoroutine()
    {
        while (!isRocketLanded)
        {
            yield return new WaitForSeconds(0.033f);
            LandingMethod();
            if (isRocketLanded)
            {
                landingCamera.enabled = false;
                Instantiate(player);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        flyAwaySpeed = 0f;
        isRocketLanded = true;
        StopCoroutine(landingCoroutine());
    }
    private void LandingMethod()
    {
        float x = flyAwaySpeed * Time.deltaTime;
        Vector3 flyVector = new Vector3(rocketRB.transform.position.x, rocketRB.position.y - x, rocketRB.position.z);
        rocketRB.MovePosition(flyVector);
    }

}
