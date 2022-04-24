using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDColor : MonoBehaviour
{

    [SerializeField] private Image[] squareColors; 

    
    public void modifyColor(int indexColor)
    {
        for(int i = 0; i < squareColors.Length; i++)
        {
            if (i == indexColor) 
            {
                squareColors[i].transform.localScale = new Vector3(1.5f, 1.5f, 0); 
            }
            else 
            {
                squareColors[i].transform.localScale = new Vector3(1, 1, 0); 
            }
        }
    }
}
