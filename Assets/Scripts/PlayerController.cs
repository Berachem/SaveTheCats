using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        
        if(grabbedCat == null)//si on voit un chat et qu'on � pas de chat dans les bras

            if (seeCat)
            {
                //on peut chopper le chat qu'on voit & s'il n'est pas dans la SafeZone
                CatController catController = seenCat.GetComponent<CatController>();
                Talker catTalker = seenCat.GetComponent<Talker>();

                if (!catController.isInSafeZone)
                {
                    indication.text = "Appuyez sur E pour saisir le chat ou C pour discuter";
                    indication.gameObject.SetActive(true);
                    if (Input.GetKeyDown("e"))
                    {
                        // V�rifie si le chat peut �tre saisi

                        grabbedCat = seenCat;
                        catController.Meow();
                        grabbedCat.GetComponent<Rigidbody>().freezeRotation = true;
                        grabbedCat.GetComponent<Animator>().SetBool("Walk",false);
                        grabbedCat.GetComponent<Animator>().SetBool("Sit", true);
                        indication.gameObject.SetActive(false);

                    }else if (Input.GetKeyDown("c") && !catTalker.isInDialogue())
                    {
                        catTalker.TriggerDialogue();
                    }
                }else //si le chat est dans la SafeZone
                {
                    indication.text = "Le chat est dans la SafeZone. Bravo !";
                    indication.gameObject.SetActive(true);
                }
   
            }
            else
            {
                indication.gameObject.SetActive(false);
            }
        else //si on a un chat dans les bras
        {
            grabbedCat.transform.position = transform.position + transform.forward;
            Vector3 directionToPlayer = (transform.position - grabbedCat.transform.position).normalized;
            grabbedCat.transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

            if (Input.GetKeyDown("e"))
            {
                putDownCat();
                
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

    public void putDownCat()
    {
        grabbedCat.GetComponent<Animator>().SetBool("Walk", true);
        grabbedCat.GetComponent<Animator>().SetBool("Sit", false);
        grabbedCat = null;
    }
}
