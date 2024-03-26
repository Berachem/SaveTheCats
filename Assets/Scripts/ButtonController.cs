using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    public InterfaceController userInterface;

    public void StartGame()
    {
        SceneManager.LoadScene("Medieval");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Resume()
    {
        userInterface.SwitchPause();
    }

    public void StartSecondLevel()
    {
        SceneManager.LoadScene("Pirates");
    }
}
