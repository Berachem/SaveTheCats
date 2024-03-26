using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public FirstPersonController camera;
    public GameObject menu;
    public GameObject userInterface;
    public TextMeshProUGUI timerText; // Texte pour le timer
    public GameObject nextLevel;
    private float startTime; // Heure de d�but pour le timer
    private bool timerActive = true; // D�termine si le timer est actif
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
        menu.SetActive(!menu.active);
        userInterface.SetActive(!userInterface.active);
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
            camera.SwitchCursorLock();
        }
    }

    public void SwitchPause()
    {
        SwitchMenu();
        camera.SwitchCursorLock();
        if (gameRunning)
        {
            timerActive = !timerActive;
        }
    }
}
