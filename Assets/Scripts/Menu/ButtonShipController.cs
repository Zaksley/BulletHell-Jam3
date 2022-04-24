using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonShipController : MonoBehaviour
{
    
    
    [SerializeField] private GameObject priceText; 
    [SerializeField] private Image shardImage; 
    public int price = 100;

    void Start()
    {
        priceText.GetComponent<TMP_Text>().text = price.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateState(string state) 
    {
        shardImage.gameObject.SetActive(false); 
        priceText.GetComponent<TMP_Text>().text = state; 
    }

}
