using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameManager GM;

    private void Update()
    {
        
        if (GM.IsGamePaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Debug.Log("I'll quit here");
        Application.Quit();
    }
}
