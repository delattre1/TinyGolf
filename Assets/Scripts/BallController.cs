using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// REF https://pastebin.com/vNLjxX6F https://www.youtube.com/watch?v=0jTPKz3ga4w

public class BallController : MonoBehaviour
{
 
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isClicked;
    private bool isMoving;
    private GameObject currentClub;
 
    [SerializeField] int force;
    [SerializeField] GameObject club;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ballLayerMask) && !isMoving){
            startPos = raycastHit.point;
            isClicked = true;
            currentClub = Instantiate(club, new Vector3(transform.position.x + 4.85f, transform.position.y, transform.position.z + 1.05f), Quaternion.identity) as GameObject;
        }
        if (Input.GetMouseButton(0)){
            Ray raycast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHitGround, float.MaxValue, groundLayerMask)){
                endPos = raycastHitGround.point;
                Vector3 angle = endPos - startPos;
                Vector3 versor = angle.normalized;
                Vector3 tacoPos = startPos + (versor);
                float force = (endPos - startPos).magnitude;
                if (force > 20f){
                    force = 20f;
                }
                float i = Mathf.InverseLerp(0f, 20f, force);
                currentClub.transform.position = new Vector3(tacoPos.x + 4.85f, tacoPos.y + 13.9f, tacoPos.z);
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, angle, 20f, 0.0f);
                currentClub.transform.eulerAngles = newDirection;
                //currentClub.transform.eulerAngles = new Vector3(i * 90f, 0f, 0f);
            }
        }
        if (Input.GetMouseButtonUp(0) && isClicked){
            Vector3 _Force = endPos - startPos;
            isClicked = false;
            rb.AddForce(_Force*-force);
            isMoving = true;
            Destroy(currentClub);
        }
        if(rb.velocity.magnitude <= .5f){
            isMoving = false;
        }
        if(rb.velocity.magnitude < 1f && isMoving){
            rb.velocity = new Vector3(0f,0f,0f);
        }
    }
}
