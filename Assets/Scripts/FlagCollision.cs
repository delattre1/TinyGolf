using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollision : MonoBehaviour
{
    private PointCounter text;

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private bool isFinal = false;
    [SerializeField] private GameObject textBox;
    
    // Start is called before the first frame update
    void Start()
    {
        text = textBox.GetComponent<PointCounter>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player"){
            text.UpdatePoints();
            particleSystem.Play();
            StartCoroutine(NextSection(col.gameObject.transform, col.gameObject));
        }
    }

    IEnumerator NextSection(Transform ballTransform, GameObject ballGameObject)
    {
        if(isFinal){
            Debug.Log("Terminou");
        }else{
            yield return new WaitForSeconds(1f);
            ballTransform.position = nextPosition;
            Rigidbody ballRigidbody = ballGameObject.GetComponent<Rigidbody>();
            ballRigidbody.velocity = new Vector3(0f,0f,0f);
        }
    }
}
