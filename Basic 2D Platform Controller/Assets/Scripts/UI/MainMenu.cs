using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public LevelLoader levelLoader;
    [SerializeField]
    public AudioManager audioManager;

    public GameObject mainMenu,
        optionsMenu,
        levelSelectMenu;

    public GameObject menuFirstSelectedButton,
        optionsFirstSelectedButton,
        levelSelectFirstSelectedButton;

    private void Awake()
    {
        audioManager.Play("hauntedkarate");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstSelectedButton);
    }

    public void LoadLevel(int levelIndex)
    {
        audioManager.StopAllSounds();
        audioManager.Play("dragonflight");
        levelLoader.LoadLevelByIndex(levelIndex);
    }

    public void OpenLevelSelectMenu()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectFirstSelectedButton);
    }

    public void CloseLevelSelectMenu()
    {
        levelSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstSelectedButton);
    }

    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstSelectedButton);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuFirstSelectedButton);
    }

    public void QuitGame()
    {
        Debug.Log("I'll quit here");
        Application.Quit();
    }
}
