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
 
    [SerializeField] int force;
    [SerializeField] AnimationCurve ac;
    [SerializeField] LineRenderer lr;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
 
    // Start is called before the first frame update
    void Start()
    {
        //lr = gameObject.GetComponent<LineRenderer>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ballLayerMask) && !isMoving){
            //lr.enabled = true;
            lr.positionCount = 2;
            startPos = raycastHit.point;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            lr.widthCurve = ac;
            lr.numCapVertices = 10;
            isClicked = true;
        }
        if (Input.GetMouseButton(0)){
            Ray raycast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHitGround, float.MaxValue, groundLayerMask)){
                endPos = raycastHitGround.point;
                lr.SetPosition(1, endPos);
            }
        }
        if (Input.GetMouseButtonUp(0) && isClicked){
            //lr.enabled = false;
            Vector3 _Force = endPos - startPos;
            float _Distance = Vector3.Distance(startPos,endPos);
            Debug.Log(_Distance);
            isClicked = false;
            rb.AddForce(_Force*-force);
            isMoving = true;
        }
        if(rb.velocity.magnitude < 1f){
            rb.velocity = new Vector3(0f,0f,0f);
        }
        if(rb.velocity.magnitude <= 0f){
            isMoving = false;
        }
    }
}
