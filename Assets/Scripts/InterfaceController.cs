using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public CustomFirstPersonController FPVcamera;
    public GameObject menu;
    public GameObject userInterface;
    public TextMeshProUGUI timerText; // Texte pour le timer
    public GameObject nextLevel;
    private float startTime; // Heure de début pour le timer
    private bool timerActive = true; // Détermine si le timer est actif
    private bool gameRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time; // Initialise le startTime avec le temps actuel
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
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
            nextLevel.SetActive(true);
            FPVcamera.SwitchCursorLock();
        }
    }

    public void SwitchPause()
    {
        SwitchMenu();
        FPVcamera.SwitchCursorLock();
        if (gameRunning)
        {
            timerActive = !timerActive;
        }
    }
}
