using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject priceText; 
    [SerializeField] private GameObject levelText; 
    [SerializeField] private Image shardImage; 
    private TextMeshProUGUI test;

    public int level = 0; 
    public int price = 200; 

    

    // Start is called before the first frame update
    void Start()
    {
        // Initiate price & levels
        priceText.GetComponent<TMP_Text>().text = price.ToString(); 
        levelText.GetComponent<TMP_Text>().text = level.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Incrementing price & level 
    public void UpdatePrice() 
    {
        price += 50; 
        level += 1;
        priceText.GetComponent<TMP_Text>().text = price.ToString(); 
        levelText.GetComponent<TMP_Text>().text = level.ToString(); 
    }

    public void UpdateMaxLevel() 
    {
        priceText.GetComponent<TMP_Text>().text = "MAX"; 
        shardImage.gameObject.SetActive(false); 

    }
}
