using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject MenuLevel;



    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        mainMenu.SetActive(false);
        MenuLevel.SetActive(true);

    }

    public void HideAll()
    {
        mainMenu.SetActive(false);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);



    }
    public void EnableTraining()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene("ArcheryScene");

    }
    public void EnableScore()
    {
        mainMenu.SetActive(false);

    }

    public void EnableBack()
    {
        MenuLevel.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void EnableEasy() 
    { 

    }

    public void EnableMedium()
    {

    }

    public void EnableHigh() 
    {

    }

    public void EnableCompetition()
    {
        
    }
}
