using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    /* Upgrades */
    [SerializeField] private ButtonController reloadButton; 
    [SerializeField] private ButtonController playerSpeedButton; 
    [SerializeField] private ButtonController laserSpeedButton; 
    [SerializeField] private ButtonController laserSizeButton; 
    [SerializeField] private ButtonController cristalGainButton; 
    private ButtonController[] buttonsUpgrade; 
    [SerializeField] private int levelMax = 10; 

    /* Spaceships */
    [SerializeField] private ButtonShipController Ship0; 
    [SerializeField] private ButtonShipController Ship1; 
    [SerializeField] private ButtonShipController Ship2; 
    [SerializeField] private ButtonShipController Ship3; 
    [SerializeField] private ButtonShipController Ship4; 
    [SerializeField] private ButtonShipController Ship5; 
    private ButtonShipController[] buttonsSpaceship;
    [SerializeField] private int[] pricesSpaceship;


    [SerializeField] private PlayerController playerControl; 

    void Start()
    {
        buttonsUpgrade = new ButtonController[5] {reloadButton, playerSpeedButton, laserSpeedButton, laserSizeButton, cristalGainButton}; 
        buttonsSpaceship = new ButtonShipController[6] {Ship0, Ship1, Ship2,Ship3,Ship4,Ship5}; 

        // Setup first ship 
        int firstShip = 0; 
        buttonsSpaceship[firstShip].UpdateState("Selected"); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuySpaceship(int indexButton) 
    {
        ButtonShipController bsc = buttonsSpaceship[indexButton]; 

        if (!playerControl.ownSpaceShip[indexButton])
        {
            // Buy 
            if (playerControl.cristalCurrency >= bsc.price) 
            {
                playerControl.cristalCurrency -= bsc.price; 
                playerControl.ownSpaceShip[indexButton] = true; 
                TakeSpaceShip(indexButton); 
                bsc.UpdateState("Selected"); 
            }
            else 
            {
                // Error sound 
            }
        }
        // Pick spaceship
        else 
        {
            TakeSpaceShip(indexButton); 
        }
    }

    private void TakeSpaceShip(int indexSpaceShip) 
    {
        playerControl.ChangeSpaceShip(indexSpaceShip); 

        for(int i=0; i<buttonsSpaceship.Length; i++)
        {
            if (playerControl.ownSpaceShip[i])
            {
                if (i == indexSpaceShip) 
                    buttonsSpaceship[i].UpdateState("Selected"); 
                else 
                    buttonsSpaceship[i].UpdateState("Select"); 
            }
        }
    }

    // Adding level if possible 
    public void IncreaseLevel(int indexButton) 
    {
        /*
        Debug.Log(buttons.Length);
        Debug.Log(indexButton);
        Debug.Log(buttons[indexButton]);
        */
        ButtonController bc = buttonsUpgrade[indexButton]; 

        if (bc.level < levelMax)
        {
            if (playerControl.cristalCurrency >= bc.price) 
            {
                bc.UpdatePrice(); 
                playerControl.cristalCurrency -= bc.price; 

                if (bc.level == levelMax)
                   bc.UpdateMaxLevel(); 
            }
        }

        else 
        {
            // Error sound 
        }

    }
}
