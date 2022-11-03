using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D hole2DCollider;
    [SerializeField] PolygonCollider2D ground2DCollider;
    [SerializeField] MeshCollider GeneratedMeshCollider;
    [SerializeField] float initialScale;
    Mesh GeneratedMesh;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(transform.hasChanged == true){
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);

        for(int i=0; i<PointPositions.Length; i++){
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if(GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }
}
