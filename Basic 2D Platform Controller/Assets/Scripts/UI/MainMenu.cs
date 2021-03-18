using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play("tonight");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<AudioManager>().Stop("tonight");
        FindObjectOfType<AudioManager>().Play("dragonflight");
    }

    public void QuitGame()
    {
        Debug.Log("I'll quit here");
        Application.Quit();
    }
}
