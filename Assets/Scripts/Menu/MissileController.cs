using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private GameObject target; 
    [SerializeField] private float speedMissile; 
    private float maxSpeedMissile = 5f; 

    

    void Start() 
    {
        target = GameObject.Find("Boss"); 
    }

    void Update()
    {
        GameObject targetBoss = GameObject.Find("TargetBoss");

        if (speedMissile < maxSpeedMissile) speedMissile += 0.2f * Time.deltaTime; 

        transform.position = Vector3.MoveTowards(this.transform.position, targetBoss.transform.position, speedMissile * Time.deltaTime);
        
        if (transform.position == targetBoss.transform.position)
        {
            Destroy(gameObject);
            target.GetComponent<BossManager>().BossHit(); 
        }
    }
}
