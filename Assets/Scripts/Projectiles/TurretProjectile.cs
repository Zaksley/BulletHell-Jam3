using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{

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
    }

    void Update() 
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime); 
    }

    IEnumerator ShootProjectile() 
    {
        while(true) 
        {
            yield return new WaitForSeconds(projectileReload);
            for(int i=0; i < nbProjectile; i++)
            {
                
                GameObject projectile; 

                //float angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)), transform.rotation);
                // Debug.Log(angle); 
                //Quaternion projectileRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                projectile = Instantiate(prefabProjectile, projectileShooter.transform.position, Quaternion.identity);
                projectile.transform.rotation = transform.rotation; 
                projectile.GetComponent<ProjectileController>().speed = projectileSpeed; 
                projectile.GetComponent<ProjectileController>().ProjectileShoot(); 
                yield return new WaitForSeconds(timeFlick);
                
            }
        }
    }
}
