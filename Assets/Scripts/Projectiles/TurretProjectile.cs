using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private Sprite[] sprite_colors; 
    public int authorizedColor = 1; 
    private string[] layersProjectile;
    private int startingLayerProjectile = 16; 

    [SerializeField] private float nbProjectile; 
    public float projectileReload; 
    [SerializeField] private GameObject prefabProjectile; 
    [SerializeField] private float projectileSpeed = 2; 
    [SerializeField] private GameObject projectileShooter; 

    public float timeFlick = 0.3f; 
    [SerializeField] private float rotateSpeed = 1f; 
    private Rigidbody2D r; 

    [SerializeField] private bool isRotativeTurret = true; 
    [SerializeField] private float angleMax = -0.15f;
    [SerializeField] private float angleMin = -0.98f;
    [SerializeField] private bool isGoingRight = true; 
    public bool canShoot = true; 

    // Start is called before the first frame update
    void Start()
    {
        
        r = GetComponent<Rigidbody2D>(); 
        layersProjectile = new string[7] {"Projectile_blue", "Projectile_green", "Projectile_navyblue", "Projectile_orange", "Projectile_purple", "Projectile_red", "Projectile_yellow" }; 
    }

    public void StartShoot() 
    {
        StartCoroutine(ShootProjectile()); 
    }

    public void StartShootFromThrower() 
    {
        StartCoroutine(ShootProjectileFromThrower()); 
    }

    void Update() 
    {
        if (isRotativeTurret) 
        {
            Vector3 rotate;  

            if (isGoingRight)
            {
                rotate = Vector3.forward * rotateSpeed * Time.deltaTime;
                if (transform.rotation.z >= angleMax) 
                {
                    rotate = -1 * Vector3.forward * rotateSpeed * Time.deltaTime;
                    isGoingRight = false; 
                }
            }
            else
            {
                rotate = -1 * Vector3.forward * rotateSpeed * Time.deltaTime;
                if (transform.rotation.z <= angleMin) 
                {
                    rotate = 1 * Vector3.forward * rotateSpeed * Time.deltaTime;
                    isGoingRight = true; 
                }
            }
            transform.Rotate(rotate, Space.World);
        }
    }

    private int getColor() 
    {
        int c = Random.Range(0, authorizedColor); 
        return c; 
    }

    IEnumerator ShootProjectileFromThrower()
    {
        yield return new WaitForSeconds(projectileReload);
            
        int color = getColor(); 
        for(int i=0; i < nbProjectile; i++)
        {
            GameObject projectile; 
            projectile = Instantiate(prefabProjectile, projectileShooter.transform.position, Quaternion.identity);
            projectile.transform.rotation = transform.rotation; 
            projectile.GetComponent<SpriteRenderer>().sprite = sprite_colors[color]; 
            projectile.GetComponent<ProjectileController>().speed = projectileSpeed; 
            projectile.GetComponent<ProjectileController>().ProjectileShoot(); 
            projectile.layer = startingLayerProjectile + color;

            yield return new WaitForSeconds(timeFlick);
            
        }
    }

    IEnumerator ShootProjectile() 
    {
        while(canShoot) 
        {
            yield return new WaitForSeconds(projectileReload);
            
            int color = getColor(); 
            for(int i=0; i < nbProjectile; i++)
            {
                GameObject projectile; 
                projectile = Instantiate(prefabProjectile, projectileShooter.transform.position, Quaternion.identity);
                projectile.transform.rotation = transform.rotation; 
                projectile.GetComponent<SpriteRenderer>().sprite = sprite_colors[color]; 
                projectile.GetComponent<ProjectileController>().speed = projectileSpeed; 
                projectile.GetComponent<ProjectileController>().ProjectileShoot(); 
                projectile.layer = startingLayerProjectile + color;

                yield return new WaitForSeconds(timeFlick);
                
            }
        }
    }
}
