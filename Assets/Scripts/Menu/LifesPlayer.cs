using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LifesPlayer : MonoBehaviour
{

    [SerializeField] private Image[] hpDots; 

    public void ShowDamage(int index) 
    {
        Debug.Log("minus hp"); 
        //hpDots[index].enabled = false; 
    }
}
