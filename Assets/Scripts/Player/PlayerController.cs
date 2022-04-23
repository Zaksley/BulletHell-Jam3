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
    [SerializeField] private Sprite[] spritesLaser; 
    public float laserSpeed; 
    public float shootReload; 


    private Rigidbody2D r; 
    public Camera cam; 

    private Vector2 mousePos; 

    private GameObject[] shooters; 

    /* Colors  */
    [SerializeField] private Sprite[] sprites; 
    private SpriteRenderer sr; 
    private int currentSpaceship; 

    private int currentColor; 
    private int currentLevel; 

    private int nbColors = 7;
    private string[] layers; 
    private int startingLayerLaser = 9;

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

        currentColor = (int) Color.RED; 
        currentLevel = 0; 
        currentSpaceship = (currentLevel+1) * currentColor; 

        sr = GetComponent<SpriteRenderer>(); 
        sr.sprite = sprites[currentSpaceship];

        layers = new string[7] {"Laser_Player_blue", "Layer_Player_green", "Layer_Player_navyblue",
                  "Layer_Player_orange", "Layer_Player_purple", "Layer_Player_red", 
                  "Layer_Player_yellow"}; 
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E))
            ChangeColor(); 
            
    }

    void ChangeColor() 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentColor = (currentColor + 1) % nbColors; 
        }
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            currentColor = ((currentColor - 1) % nbColors) > 0 ? (currentSpaceship - 1) % nbColors : nbColors - 1;
        }

        currentSpaceship = (currentLevel+1) * currentColor;     
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
            laser.layer = startingLayerLaser + currentColor; 
            laser.GetComponent<SpriteRenderer>().sprite = spritesLaser[currentColor];

            Debug.Log(startingLayerLaser + currentColor);

            LaserController lc = laser.GetComponent<LaserController>(); 
            lc.laserSpeed = laserSpeed; 
            
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
