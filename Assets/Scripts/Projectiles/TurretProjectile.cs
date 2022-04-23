using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private Sprite[] sprite_colors; 
    private string[] layersProjectile;
    private int startingLayerProjectile = 16; 

    [SerializeField] private float nbProjectile; 
    [SerializeField] private float projectileReload; 
    [SerializeField] private GameObject prefabProjectile; 
    [SerializeField] private float projectileSpeed = 2; 
    [SerializeField] private GameObject projectileShooter; 

    private float timeFlick = 0.3f; 
    [SerializeField] private float rotateSpeed = 1f; 
    private Rigidbody2D r; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectile()); 
        r = GetComponent<Rigidbody2D>(); 
        layersProjectile = new string[7] {"Projectile_blue", "Projectile_green", "Projectile_navyblue", "Projectile_orange", "Projectile_purple", "Projectile_red", "Projectile_yellow" }; 
    }

    void Update() 
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime); 
    }

    private int getColor() 
    {
        int c = Random.Range(0, sprite_colors.Length); 
        return c; 
    }

    IEnumerator ShootProjectile() 
    {
        while(true) 
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
