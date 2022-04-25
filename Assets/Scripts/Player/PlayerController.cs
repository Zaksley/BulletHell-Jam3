using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    
    /* Movement */
    float rotation; 
    public float speed; 
    [SerializeField] float rotateSpeed; 
    private Vector2 movement; 

    
    /* Sound */
    public AudioSource s; 
    public AudioClip shoot; 
    public AudioClip mainMusic; 

    /*  Shoot */ 
    [SerializeField] private GameObject prefabLaser; 
    [SerializeField] private Sprite[] spritesLaser; 
    [SerializeField] private Sprite[] spritesCourbeLaser; 
    public float laserSpeed; 
    public float laserSize; 
    public float shootReload; 


    private Rigidbody2D r; 
    public Camera cam; 

    private Vector2 mousePos; 

    private GameObject[] shooters; 
    private int[] nbShooters; 

    private GameObject[] courbeShooters; 
    private int[] nbCourbeShooters; 

    /* Colors  */
    [SerializeField] private Sprite[] sprites; 
    private SpriteRenderer sr; 
    [SerializeField] private HUDColor hudColor; 
    private int currentSpaceship; 

    private int currentColor; 
    public int currentLevel; 

    public bool[] ownSpaceShip; 

    private int nbColors = 7;
    private string[] layers; 
    private int startingLayerLaser = 9;


    /* Buy */ 
    public int cristalCurrency = 500000;
    public int cristalGain = 50; 
    public GameObject cristalText; 

    /* Lives */
    private int lives; 
    [SerializeField] private int StartLives = 10; 
    [SerializeField] private GameObject[] dots; 

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
        lives = StartLives; 

        s.PlayOneShot(mainMusic);

        r = GetComponent<Rigidbody2D>(); 
        shooters = GameObject.FindGameObjectsWithTag("Shooter"); 
        nbShooters = new int[6] {1, 2, 3, 4, 5, 6}; 

        courbeShooters = GameObject.FindGameObjectsWithTag("CourbeShooter"); 
        nbCourbeShooters = new int[6] {0, 0, 0, 0, 0, 0}; 

        StartCoroutine(AutomaticShoot()); 

        currentColor = (int) Color.BLUE; 
        currentLevel = 0; 
        currentSpaceship = currentLevel * nbColors + currentColor;
        ownSpaceShip = new bool[6] {true, false, false, false, false, false}; 

        sr = GetComponent<SpriteRenderer>(); 
        sr.sprite = sprites[currentSpaceship];
        hudColor.modifyColor(currentColor);

        layers = new string[7] {"Laser_Player_blue", "Layer_Player_green", "Layer_Player_navyblue",
                  "Layer_Player_orange", "Layer_Player_purple", "Layer_Player_red", 
                  "Layer_Player_yellow"}; 


        cristalText.GetComponent<TMP_Text>().text = cristalCurrency.ToString();

        dots = GameObject.FindGameObjectsWithTag("LifePlayer");
        
    }


    void Update()
    {
        for(int i=0; i<StartLives; i++)
        {
            if (i >= lives)
                dots[i].GetComponent<Image>().enabled = false; 
        }

        if (lives <= 0)
        {
            DeathPlayer();
        }

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
            currentColor = ((currentColor - 1) % nbColors) >= 0 ? (currentSpaceship - 1) % nbColors : nbColors - 1;
        }

        currentSpaceship = currentLevel * nbColors + currentColor;   
        hudColor.modifyColor(currentColor);
        sr.sprite = sprites[currentSpaceship];
    }

    public void ChangeSpaceShip(int indexSpaceship) 
    {
        currentLevel = indexSpaceship; 
        currentSpaceship = currentLevel * nbColors + currentColor;
        sr.sprite = sprites[currentSpaceship];
    }

    public void ChangeCrystalUI() 
    {
        cristalText.GetComponent<TMP_Text>().text = cristalCurrency.ToString();
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

        int indexShoot = 0; 
        int indexCourbeShoot = 0; 
        int search = 0; 
        
        // Gets index from where to start
        while(search != currentLevel)
        {
            indexShoot += nbShooters[search]; 
            indexCourbeShoot += nbCourbeShooters[search]; 
            search++; 
        }

        s.PlayOneShot(shoot); 

        for(int i=0; i<nbShooters[currentLevel]; i++)
        {
            GameObject laser; 

            laser = Instantiate(prefabLaser, shooters[indexShoot+i].transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().rotation = r.rotation; 
            laser.transform.rotation = transform.rotation; 
            laser.layer = startingLayerLaser + currentColor; 
            laser.transform.localScale = new Vector3(laserSize, laserSize, laserSize); 
            laser.GetComponent<SpriteRenderer>().sprite = spritesLaser[currentColor];

            LaserController lc = laser.GetComponent<LaserController>(); 
            lc.laserSpeed = laserSpeed; 
        }

        /*
        for(int i=0; i<nbCourbeShooters[currentLevel]; i++)
        {
            GameObject courbeLaser; 

            courbeLaser = Instantiate(prefabLaser, shooters[indexCourbeShoot+i].transform.position, Quaternion.identity);
            courbeLaser.GetComponent<Rigidbody2D>().rotation = r.rotation; 
            courbeLaser.transform.rotation = transform.rotation; 
            courbeLaser.layer = startingLayerLaser + currentColor; 
            courbeLaser.GetComponent<SpriteRenderer>().sprite = spritesCourbeLaser[currentColor];

            LaserController lc = courbeLaser.GetComponent<LaserController>(); 
            lc.laserSpeed = laserSpeed; 
        }
        */
    }

    IEnumerator AutomaticShoot()
    {
        while(true) 
        {
            yield return new WaitForSeconds(shootReload);
            Shoot(); 
        }
    }

    public void TakeDamage() 
    {
        lives -= 1; 
    }

    private void DeathPlayer()
    {
        SceneManager.LoadScene("Defeat"); 
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject); 
            TakeDamage(); 
        }
    }
}
