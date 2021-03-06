using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed;
    private float basicSpeed; 
    [SerializeField] private GameObject target; 
  
    [SerializeField] private Rigidbody2D r_projectile; 
    [SerializeField] private PlayerController player; 
    Vector2 lastVelocity; 

    void Start() 
    {
        basicSpeed = 2.0f;
    }

    void Update()
    {   
        lastVelocity = r_projectile.velocity; 

    }

    public void ProjectileShoot() 
    {
        r_projectile.velocity =  transform.right * speed; 
    }

    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Bord"))
        {
            var speed = lastVelocity.magnitude; 
            var direction = Vector2.Reflect(lastVelocity.normalized, other.contacts[0].normal); 

            r_projectile.velocity = direction * Mathf.Max(speed, basicSpeed);
        }
    }   
}
