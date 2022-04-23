using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

    [SerializeField] private float nbProjectile; 
    [SerializeField] private float projectileReload; 
    [SerializeField] private GameObject prefabProjectile; 
    [SerializeField] private float projectileSpeed = 2; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectile()); 
    }

    IEnumerator ShootProjectile() 
    {
        while(true) 
        {
            yield return new WaitForSeconds(projectileReload);
            for(int i=0; i < nbProjectile; i++)
            {
                
                GameObject projectile; 
                projectile = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
                //projectile.GetComponent<Rigidbody2D>().rotation = r.rotation + 90f; 
                projectile.GetComponent<ProjectileController>().speed = projectileSpeed; 
                
            }
        }
    }
}
