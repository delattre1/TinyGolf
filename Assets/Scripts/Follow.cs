using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    private float currentAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = ball.transform.position;
        transform.position = new Vector3(position.x, 0f, position.z);

        transform.eulerAngles = new Vector3(0f, currentAngle, 0f);
        if(Input.GetKeyDown(KeyCode.A)){
            currentAngle += 45f;
        }else if(Input.GetKeyDown(KeyCode.D)){
            currentAngle -= 45f;
        }
    }
}
