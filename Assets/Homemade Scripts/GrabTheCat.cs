using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GrabTheCat : MonoBehaviour
{

    private bool seeCat;
    private GameObject seenCat;
    private GameObject grabbedCat;
    public TextMeshProUGUI indication;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //si on voit un chat et qu'on à pas de chat dans les bras
        if(grabbedCat == null)

            if (seeCat)
            {
                //on peut chopper le chat qu'on voit
                indication.gameObject.SetActive(true);
                if (Input.GetKeyDown("e")) {
                    grabbedCat = seenCat;
                    grabbedCat.GetComponent<Rigidbody>().freezeRotation = true;
                    indication.gameObject.SetActive(false);
                }
            }
            else
            {
                indication.gameObject.SetActive(false);
            }
        else //si on a un chat dans les bras
        {
            grabbedCat.transform.position = transform.position + transform.forward;
            grabbedCat.transform.rotation = transform.rotation;
            
            if (Input.GetKeyDown("e"))
            {
                grabbedCat.GetComponent<Rigidbody>().freezeRotation = false;
                grabbedCat = null;
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabable"))
        {
            seeCat = true;
            seenCat = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grabable"))
        {
            seeCat = false;
            seenCat = null;
        }
    }
}
