using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 direction, currentPosition, positionPlayerAim;
    Ray aimRay;
    RaycastHit impact;
    public LayerMask GroundMask;
    float vel = 15;
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        direction = new Vector3(xAxis, 0, zAxis);

        if (direction != Vector3.zero) {
            GetComponent<Animator>().SetBool("isRunning", true);
        }
        else {
            GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    void FixedUpdate() { //padrao execucao a cada 0.2 segundos
        currentPosition = GetComponent<Rigidbody>().position;

        GetComponent<Rigidbody>().MovePosition(
            currentPosition + direction * vel * Time.deltaTime
        );

        aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(aimRay.origin, aimRay.direction*100, Color.red);

        if (Physics.Raycast(aimRay, out impact, 100, GroundMask)) {
            positionPlayerAim = impact.point - transform.position;
            positionPlayerAim.y = transform.position.y;

            Quaternion newRotation = Quaternion.LookRotation(positionPlayerAim);
            GetComponent<Rigidbody>().MoveRotation(newRotation);
        }




    }
}
