using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOverScript : MonoBehaviour
{

    [SerializeField] private PauseGameScript pauseGame;
    

    public void GameLost()
    {

        pauseGame.canUsePauseMenu = false;
        pauseGame.gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }


    public void GameWon()
    {

        pauseGame.canUsePauseMenu = false;
        pauseGame.victoryPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

}
