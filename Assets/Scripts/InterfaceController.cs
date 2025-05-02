using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class InterfaceController : MonoBehaviour
{
    public GameObject menu;
    public GameObject userInterface;
    public TextMeshProUGUI timerText; // Texte pour le timer
    private float startTime; // Heure de début pour le timer
    private bool timerActive = true; // Détermine si le timer est actif
    private bool gameRunning = true;

    public InputActionReference pauseAction;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; // Initialise le startTime avec le temps actuel
        //nextLevel.SetActive(false); // Désactive le texte pour le niveau suivant
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.action.WasPressedThisFrame())
        {
            SwitchPause();
        }

        if (timerActive)
        {
            UpdateTimerText();
        }


    }

    public void SwitchMenu()
    {
        menu.SetActive(!menu.activeSelf);
        userInterface.SetActive(!userInterface.activeSelf);
    }
   

    private void UpdateTimerText()
    {
        if (timerActive)
        {
            float timeSinceStart = Time.time - startTime;
            int minutes = Mathf.FloorToInt(timeSinceStart / 60);
            int seconds = Mathf.FloorToInt(timeSinceStart % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StopTimer()
    {
        if (gameRunning)
        {
            timerActive = false;
            gameRunning = false;
           // nextLevel.SetActive(true);
            
        }
    }

    public void SwitchPause()
    {
        SwitchMenu();
       
        if (gameRunning)
        {
            timerActive = !timerActive;
        }
    }
}
