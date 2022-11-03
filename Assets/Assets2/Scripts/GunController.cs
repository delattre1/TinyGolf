using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject GunPositionTracker;
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) {
            Instantiate(Bullet, GunPositionTracker.transform.position, GunPositionTracker.transform.rotation);
        }
        
    }
}
