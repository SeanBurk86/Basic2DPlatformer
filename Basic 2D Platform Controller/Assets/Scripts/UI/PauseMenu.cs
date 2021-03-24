using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI,
        pauseMenuFirstSelectedButton;

    [SerializeField]
    private PlayerInputHandler playerInputHandler;

    public bool pauseToggle;

    public void CheckPauseToggle()
    {
        if (pauseToggle) pauseToggle = false;
        else pauseToggle = true;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstSelectedButton);
        Time.timeScale = 0;
    }
    private void ContinueGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GotoMainMenu()
    {
        FindObjectOfType<AudioManager>().StopAllSounds();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("I should quit... you're probably in the editor");
        Application.Quit();
    }
}
