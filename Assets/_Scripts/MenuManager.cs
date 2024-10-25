using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject MainMenu;

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
        MainMenu.SetActive(false);
        canvasShowed.SetActive(true);
    }

    public void BackToMainMenu(GameObject currentCanvas)
    {
        currentCanvas.SetActive(false);
        MainMenu.SetActive(true);
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
