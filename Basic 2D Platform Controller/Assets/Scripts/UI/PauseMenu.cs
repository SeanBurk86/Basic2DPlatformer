using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private PlayerInputHandler playerInputHandler;

    private bool pauseToggle;

    private void Start()
    {
    }
    void Update()
    {
        CheckPauseToggle();
    }

    private void CheckPauseToggle()
    {
        pauseToggle = playerInputHandler.PauseToggle;
        if (pauseToggle)
        {
            PauseGame();
        }
        else if(!pauseToggle)
        {
            ContinueGame();
        }
    }
    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        playerInputHandler.UsePauseToggle();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("I should quit... you're probably in the editor");
        Application.Quit();
    }
}
