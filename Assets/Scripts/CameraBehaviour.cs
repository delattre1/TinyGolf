using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private float currentAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(45f, 45f, currentAngle);
        if(Input.GetKeyDown(KeyCode.P)){
            currentAngle += 45f;
        }else if(Input.GetKeyDown(KeyCode.O)){
            currentAngle -= 45f;
        }
    }
}
