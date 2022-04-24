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

    public void InitializePrice(int value) 
    {
        price = value; 
        priceText.GetComponent<TMP_Text>().text = price.ToString();
        levelText.GetComponent<TMP_Text>().text = level.ToString(); 
    }

    // Incrementing price & level 
    public void UpdatePrice(int newPrice) 
    {
        price = newPrice; 
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
