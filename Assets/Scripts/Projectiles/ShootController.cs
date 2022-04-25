using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    [SerializeField] private TurretProjectile[] turrets; 
    [SerializeField] private TurretProjectile[] throwers;     


    private int nbThrowerActive = 0; 
    
    private float secondsMoreColor; 
    private float secondsMoreTurret; 
    private float secondsAddThrower; 
    private float secondsShootThrower; 

    void Start()
    {
            // First turret shooting 
        turrets[0].StartShoot(); 

        secondsMoreColor = 40f; 
        secondsMoreTurret = 20f; 

        secondsAddThrower = 60f;
        secondsShootThrower = 45f; 

        StartCoroutine(moreColor(turrets));
        StartCoroutine(applyStartShoot(turrets[1])); 

        StartCoroutine(addMoreThrower()); 
        StartCoroutine(startShootThrower());
        StartCoroutine(moreColor(throwers));

        for(int i=0; i<throwers.Length; i++)
        {
            throwers[i].canShoot = false; 
            throwers[i].projectileReload = 0.1f; 
            throwers[i].timeFlick = 0.2f; 
        }
    }

    IEnumerator addMoreThrower()
    {
        var addThrower = true; 
        while(addThrower) 
        {
            yield return new WaitForSeconds(secondsAddThrower); 
            if (nbThrowerActive < throwers.Length) 
            {
                nbThrowerActive++; 
            }

            if (nbThrowerActive == throwers.Length)
                addThrower = false; 
        }
    }


    IEnumerator startShootThrower()
    {
        while(true) 
        {
            yield return new WaitForSeconds(secondsShootThrower); 
            List<int> indexShooter = new List<int> {0, 1, 2, 3}; 

            TurretProjectile thrower;

            for(int i=0; i<nbThrowerActive; i++)
            {
                int chosed = Random.Range(0, indexShooter.Count);

                
                thrower = throwers[indexShooter[chosed]]; 
                indexShooter.Remove(indexShooter[chosed]); 
                thrower.StartShootFromThrower(); 

                /*
                for(int j=0; j<indexShooter.Count;j++) 
                {
                    Debug.Log("ELEMENT DANS INDEX: " + indexShooter[j].ToString());
                }*/
            }

            // Stop thrower frorm shooting
            /*
            for(int i=0; i<throwers.Length; i++)
            {
                throwers[i].canShoot = false; 
            }
            */
        }
    }

    IEnumerator applyStartShoot(TurretProjectile turret) 
    {
        yield return new WaitForSeconds(secondsMoreTurret); 
        turret.StartShoot(); 
    }

    IEnumerator moreColor(TurretProjectile[] turrets) 
    {
        bool addColor = true; 
        while(addColor) 
        {
            yield return new WaitForSeconds(secondsMoreColor); 

            for(int i=0; i<turrets.Length; i++)
            {
                turrets[i].authorizedColor++; 
            }

            if (turrets[0].authorizedColor == 7)
                addColor = false; 
        }
    }
}
