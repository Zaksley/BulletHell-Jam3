using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePaused : MonoBehaviour
{

    public static bool GameIsPaused = false; 
    public GameObject pauseMenuUI; 

    void Start()
    {
        pauseMenuUI.SetActive(false); 
        GameIsPaused = false; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) 
            {
                Resume(); 
            }
            else
            {
                Pause(); 
            }
        }
    }

    void Pause() 
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;
        GameIsPaused = true; 
    }

    void Resume() 
    {
        pauseMenuUI.SetActive(false); 
        GameIsPaused = false; 
        Time.timeScale = 1f;
    }

}
