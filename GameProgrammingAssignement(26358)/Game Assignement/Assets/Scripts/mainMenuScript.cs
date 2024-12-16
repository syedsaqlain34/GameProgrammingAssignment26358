using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public GameObject L2Lock;
    public GameObject L3Lock;

    public GameObject playGamePanel;
    public GameObject levelSelectorPanel;

    public int levelWins;

    void Start()
    {
        levelWins = PlayerPrefs.GetInt("levelsWon");
        Debug.Log("Level Wins Retrieved: " + levelWins);
    }

    void Update()
    {
        if (levelWins == 0)
        {
            L2Lock.SetActive(true);
        }
        else if (levelWins == 1)
        {
            L2Lock.SetActive(false);
        }
       
    }

    public void playGame()
    {
        playGamePanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
    }

    public void level1()
    {
        SceneManager.LoadScene(1);
    }

    public void level2()
    {
        SceneManager.LoadScene(2);
    }

  

    public void resetGame()
    {
        PlayerPrefs.SetInt("levelsWon", 0);
        PlayerPrefs.Save(); 
        SceneManager.LoadScene(0);
    }
}
