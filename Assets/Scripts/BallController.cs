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
    private GameObject line;
    private float force = 0f;
 
    [SerializeField] int _Force;
    [SerializeField] float damping;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask ballLayerMask;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Vector3 offset;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        line = gameObject.transform.GetChild(0).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // Criação de batida
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ballLayerMask) && !isMoving){
            startPos = raycastHit.point;
            isClicked = true;
            line.SetActive(true);
        }
        // Posicionamento e redimensionamento de linha de força
        if (Input.GetMouseButton(0) && isClicked && !isMoving){
            Ray raycast = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHitGround, float.MaxValue, groundLayerMask)){
                endPos = raycastHitGround.point;
                Vector3 vector = endPos - startPos;
                Vector3 versor = vector.normalized;
                Vector3 linePos = startPos + (versor);
                force = (endPos - startPos).magnitude;
                if (force > 20f){force = 20f;}
                line.transform.localScale = new Vector3(force, line.transform.localScale.y, line.transform.localScale.z);
                float hitAngle = force*90f/20f;
                Vector3 newPosition = new Vector3(linePos.x, 0.5f, linePos.z);
                line.transform.position = newPosition;
                // Vector3 targetPosition = new Vector3(transform.position.x, line.transform.position.y, transform.position.z);
                // line.transform.LookAt(targetPosition);

                var lookPos = transform.position - line.transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                line.transform.rotation = Quaternion.Slerp(line.transform.rotation, rotation, Time.deltaTime * damping);
            }
        }
        // Finalização de batida
        if (Input.GetMouseButtonUp(0) && isClicked){
            isClicked = false;
            isMoving = true;
            rb.AddForce(_Force*-(endPos - startPos));
            line.SetActive(false);
        }
        // Finalização de movimento da bola
        if(rb.velocity.magnitude <= .5f){
            isMoving = false;
        }
        if(rb.velocity.magnitude < 1f && isMoving){
            rb.velocity = new Vector3(0f,0f,0f);
        }
    }
}
