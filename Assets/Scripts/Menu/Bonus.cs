using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    [SerializeField] private GameObject prefabMissile; 
    [SerializeField] private GameObject target; 


    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMissile()
    {

        Vector2 lookDir = target.transform.position - transform.position; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 180; 

        GameObject missile = Instantiate(prefabMissile, transform.position, Quaternion.identity);
        missile.transform.Rotate(0, 0, angle, Space.World);         
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Destroy(gameObject); 
        CreateMissile(); 
    }
}
