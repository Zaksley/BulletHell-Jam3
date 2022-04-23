using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
  
    [SerializeField] private Rigidbody2D r_projectile; 


    void Update()
    {   
        r_projectile.velocity =  transform.right * speed; 
    }

    /*
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject); 
        }
    }*/
}
