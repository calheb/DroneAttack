using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI; //death menu

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && deathMenuUI.activeSelf) // Allows player to quickly play again by hitting space bar on the death menu
        {
            PlayAgain();
        }
    }

    void Pause() // brings up the death menu 
    {
        deathMenuUI.SetActive(true); 
        Time.timeScale = 1f;
    }

    public void LoadMenu() // brings up main menu
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void QuitGame() // closes application
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void PlayAgain() // reloads the main game scene 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
}
