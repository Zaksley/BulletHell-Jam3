using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public float laserSpeed;
  
    [SerializeField] private Rigidbody2D r_laser; 


    void Update()
    {   
        r_laser.velocity =  transform.right * laserSpeed; 
    }
}
