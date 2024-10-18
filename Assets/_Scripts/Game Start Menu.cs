using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;


    [Header("Main Menu Buttons")]
    public Button JouerButton;
    public Button EntrainementButton;
    public Button ScoreButton;
    public Button QuitterButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        JouerButton.onClick.AddListener(StartGame);
        EntrainementButton.onClick.AddListener(EnableOption);
        ScoreButton.onClick.AddListener(EnableAbout);
        QuitterButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);

    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);

    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);

    }
}
