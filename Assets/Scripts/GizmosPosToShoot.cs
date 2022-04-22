using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosPosToShoot : MonoBehaviour
{
    [SerializeField] private float radius; 


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Gizmos.DrawSphere(transform.position, radius); 
    }
}
