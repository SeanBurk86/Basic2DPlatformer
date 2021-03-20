using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public LevelLoader levelLoader;
    [SerializeField]
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager.Play("tonight");
    }
    public void PlayGame()
    {
        audioManager.Stop("tonight");
        audioManager.Play("dragonflight");
        levelLoader.LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("I'll quit here");
        Application.Quit();
    }
}
