using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseGameScript : MonoBehaviour
{

    public Transform pausePanel;
    public Transform gameOverPanel;
    public Transform victoryPanel;
    public bool canUsePauseMenu;


    public void Awake()
    {

        canUsePauseMenu = true;

    }


    // Open given panel and stop gametime or close panel and start gametime again when pressed esc
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && canUsePauseMenu == true)
        {

            if (pausePanel.gameObject.activeInHierarchy == false)
            {

                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0;

            }

            else
            {

                pausePanel.gameObject.SetActive(false);
                Time.timeScale = 1;

            }

        }

    }


    // Unpause game with button instead of esc
    public void UnPauseGame()
    {

        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;

        if (canUsePauseMenu == false)
        {

            canUsePauseMenu = true;

        }

    }

}
