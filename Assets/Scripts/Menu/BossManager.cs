using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class BossManager : MonoBehaviour
{
    [SerializeField] GameObject prefabBonus; 

    [SerializeField] private float xMax = 5f; 
    [SerializeField] private float xMin = -5f; 
    [SerializeField] private float yMax = 3f; 
    [SerializeField] private float yMin = -3f; 
    [SerializeField] private int secondsRespawnBonus = 5;
    [SerializeField] private int bossLives; 

    [SerializeField] private Sprite[] bossSprites;

    void Start()
    {
        StartCoroutine(spawnBonus());

        bossLives = 18;
        GetComponent<SpriteRenderer>().sprite = bossSprites[bossLives-1];
    }


    public void BossHit() 
    {
        bossLives -= 1;

        if (bossLives <= 0)
        {
            SceneManager.LoadScene("Win");
        } 
        else 
        {
            this.GetComponent<SpriteRenderer>().sprite = bossSprites[bossLives-1];
        }
    }

    IEnumerator spawnBonus()
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsRespawnBonus); 

            float x = Random.Range(xMin, xMax); 
            float y = Random.Range(yMin, yMax); 
            GameObject bonus = Instantiate(prefabBonus, new Vector3(x, y, 0), Quaternion.identity);
        }
    }


}
