using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private PlayerInputHandler playerInputHandler;

    private bool gameIsPaused;

    private void Start()
    {
        gameIsPaused = false;
    }
    void Update()
    {
        CheckPauseToggle();
    }

    private void CheckPauseToggle()
    {
        if (playerInputHandler.PauseToggle && !gameIsPaused)
        {
            PauseGame();
        }
        else if(!playerInputHandler.PauseToggle && gameIsPaused)
        {
            ContinueGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I should've quit... you're probably in the editor");
    }
}
