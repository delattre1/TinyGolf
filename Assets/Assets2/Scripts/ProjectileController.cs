using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    Vector3 currentPosition;
    public float vel = 50;

    void FixedUpdate() {
        currentPosition = GetComponent<Rigidbody>().position;

        GetComponent<Rigidbody>().MovePosition(
            currentPosition + transform.forward * vel * Time.deltaTime
        );

    }

    void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Enemy") {
            Destroy(obj.gameObject);
        }
        Destroy(gameObject);
    }
}
