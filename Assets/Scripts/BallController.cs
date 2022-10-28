using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
// REF https://pastebin.com/vNLjxX6F https://www.youtube.com/watch?v=0jTPKz3ga4w

public class BallController : MonoBehaviour
{
 
    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 startForce;
    private Vector3 endForce;
    private bool isClicked;
    private bool isMoving;
    private bool isPickingForce;
    private GameObject currentClub;
    private GameObject newClub;
 
    [SerializeField] int force;
    [SerializeField] GameObject club;
    [SerializeField] GameObject rotateClub;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Vector3 offset;
 
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
            currentClub = Instantiate(club, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        }
        if (Input.GetMouseButton(0) && isClicked){
            Ray raycast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHitGround, float.MaxValue, groundLayerMask)){
                endPos = raycastHitGround.point;
                Vector3 vector = endPos - startPos;
                Vector3 versor = vector.normalized;
                Vector3 tacoPos = startPos + (versor);
                float force = (endPos - startPos).magnitude;
                if (force > 20f){force = 20f;}
                float hitAngle = force*90f/20f;
                Vector3 newPosition = new Vector3(tacoPos.x, 0.5f, tacoPos.z);
                currentClub.transform.position = newPosition;
                Vector3 targetPosition = new Vector3(transform.position.x, currentClub.transform.position.y, transform.position.z);
                currentClub.transform.LookAt(targetPosition);
            }
        }
        if (Input.GetMouseButtonUp(0) && isClicked){
            isClicked = false;
            isPickingForce = true;
            Ray rayForce_0 = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(rayForce_0, out RaycastHit raycastHit0, float.MaxValue)){startForce = raycastHit0.point;}
            newClub = Instantiate(rotateClub, new Vector3(currentClub.transform.position.x - 5.1f, currentClub.transform.position.y + 13.75f, currentClub.transform.position.z - 0.2f), Quaternion.identity) as GameObject;
        }
        if (isPickingForce){
            //Destroy(currentClub);
            Ray rayForce_1 = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(rayForce_1, out RaycastHit raycastHit1, float.MaxValue)){endForce = raycastHit1.point;}
            Vector3 _Force = endForce - startForce;
            float force = _Force.magnitude;
            float hitAngle = force*90f/20f;
            newClub.transform.rotation = Quaternion.AngleAxis(hitAngle, Vector3.right);
            if (Input.GetMouseButtonDown(0)){    
                isMoving = true;
                rb.AddForce(_Force*-force);
                isPickingForce = false;
                Destroy(newClub);
            }
        }
        if(rb.velocity.magnitude <= .5f){
            isMoving = false;
        }
        if(rb.velocity.magnitude < 1f && isMoving){
            rb.velocity = new Vector3(0f,0f,0f);
        }
    }
}
