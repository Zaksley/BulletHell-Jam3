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
    public float laserSpeed; 
    public float shootReload; 


    private Rigidbody2D r; 
    public Camera cam; 

    private Vector2 mousePos; 

    private GameObject[] shooters; 

    // Colors 
    [SerializeField] private Sprite[] sprites; 
    private SpriteRenderer sr; 
    private int currentSpaceship; 
    private int nbColors = 7;

    enum Color 
    {
        BLUE = 0,
        GREEN = 1,
        NAVY_BLUE = 2,
        ORANGE = 3,
        PURPLE = 4,
        RED = 5,
        YELLOW = 6
    }; 

    void Start()
    {
        r = GetComponent<Rigidbody2D>(); 
        shooters = GameObject.FindGameObjectsWithTag("Shooter"); 
        StartCoroutine(AutomaticShoot()); 

        currentSpaceship = (int) Color.RED; 
        sr = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetKeyDown(KeyCode.Space))
            Shoot(); 

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E))
            ChangeColor(); 
            
    }

    void ChangeColor() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSpaceship = (currentSpaceship + 1) % nbColors; 
        }
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            currentSpaceship = ((currentSpaceship - 1) % nbColors) > 0 ? (currentSpaceship - 1) % nbColors : nbColors - 1;
        }

        sr.sprite = sprites[currentSpaceship];
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

        for (int i=0; i<shooters.Length; i++)
        {
            GameObject laser; 
            laser = Instantiate(prefabLaser, shooters[i].transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().rotation = r.rotation + 90f; 
            laser.GetComponent<LaserController>().laserSpeed = laserSpeed; 
        }
    }

    IEnumerator AutomaticShoot()
    {
        while(true) 
        {
            yield return new WaitForSeconds(shootReload);
            Shoot(); 
        }

    }

}
