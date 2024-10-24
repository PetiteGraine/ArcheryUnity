using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;

    private void LoadGame()
    {
        SceneManager.LoadScene("Archery");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisplayMenu(GameObject canvasShowed)
    {
        mainMenu.SetActive(false);
        canvasShowed.SetActive(true);

    }

    public void BackToMainMenu(GameObject currentCanvas)
    {
        currentCanvas.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartEasyMode()
    {
        PlayerPrefs.SetString("mode", "easy");
        LoadGame();
    }

    public void StartMediumMode()
    {
        PlayerPrefs.SetString("mode", "medium");
        LoadGame();
    }

    public void StartHardMode()
    {
        PlayerPrefs.SetString("mode", "hard");
        LoadGame();
    }

    public void StartTrainingMode()
    {
        PlayerPrefs.SetString("mode", "training");
        LoadGame();
    }
}
