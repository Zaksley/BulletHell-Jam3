using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    /* Movement */
    float rotation; 
    [SerializeField] float speed; 
    [SerializeField] float rotateSpeed; 
    private Vector2 movement; 

    

    /*  Shoot */ 
    [SerializeField] private GameObject prefabLaser; 
    private bool isShooting;
    public float laserSpeed; 


    private Rigidbody2D r; 
    public Camera cam; 

    private Vector2 mousePos; 

    private GameObject[] shooters; 

    void Start()
    {
        r = GetComponent<Rigidbody2D>(); 
        isShooting = false; 
       // positionToShoot = GetComponentInChildren<GameObject>(); 
       shooters = GameObject.FindGameObjectsWithTag("Shooter"); 
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); 
        }
    }

    void FixedUpdate() 
    {
        Move(); 
        Rotate(); 

    }

    private void Move()
    {
        float magnitude = Mathf.Clamp01(movement.magnitude); 
        movement.Normalize(); 

        transform.Translate(movement * speed * magnitude * Time.deltaTime, Space.World); 
    }

    private void Rotate()
    {
        Vector2 lookDir = mousePos - r.position; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; 
        r.rotation = angle; 
    }

    private void Shoot() 
    {
        //Instantiate(laser, positionToShoot.transform.position, Quaternion.identity); 
        for (int i=0; i<shooters.Length; i++)
        {
            GameObject laser; 
            laser = Instantiate(prefabLaser, shooters[i].transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().rotation = r.rotation + 90f; 
            laser.GetComponent<LaserController>().laserSpeed = laserSpeed; 
        }
    }
}
