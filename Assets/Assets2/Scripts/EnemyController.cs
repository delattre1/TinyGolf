using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public float vel = 5;
    float playerDistance;
    Vector3 currentPosition, playerPosition, playerDirection;

    void FixedUpdate() { //padrao execucao a cada 0.2 segundos
        currentPosition = GetComponent<Rigidbody>().position;
        playerPosition  = Player.GetComponent<Rigidbody>().position;
        playerDirection = (playerPosition - currentPosition).normalized;
        playerDistance  = Vector3.Distance(currentPosition, playerPosition);
        
        //Make zombie look at player
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        GetComponent<Rigidbody>().MoveRotation(rotation); 

        if (playerDistance > 2.5) {
            GetComponent<Rigidbody>().MovePosition(
                currentPosition + playerDirection * vel * Time.deltaTime
            );
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
        else {
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

    }
}
