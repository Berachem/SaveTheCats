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
    private bool gameRunning = true;

    public InputActionReference pauseAction;

    void Start()
    {
        pauseAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.action != null && pauseAction.action.WasPressedThisFrame())
        {
            Debug.Log("Pause action déclenchée !");
            SwitchPause();
        }
    }

    public void SwitchMenu()
    {
        menu.SetActive(!menu.activeSelf);
        userInterface.SetActive(!userInterface.activeSelf);
    }

    public void SwitchPause()
    {
        SwitchMenu();
    }
}
