using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public float laserSpeed;
    public AudioSource s; 
    public AudioClip hit; 
  
    [SerializeField] private Rigidbody2D r_laser; 
    private GameObject player; 


    private void Start() 
    {
        s = GameObject.Find("Audio2").GetComponent<AudioSource>(); 
    }

    void Update()
    {   
        r_laser.velocity =  transform.up * laserSpeed; 
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Bord"))
        {
            Destroy(gameObject); 
        }
        else if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject); 
            Destroy(other.gameObject);

            player.GetComponent<PlayerController>().cristalCurrency += player.GetComponent<PlayerController>().cristalGain; 
            player.GetComponent<PlayerController>().ChangeCrystalUI(); 

            s.PlayOneShot(hit); 
        }
    }
}
