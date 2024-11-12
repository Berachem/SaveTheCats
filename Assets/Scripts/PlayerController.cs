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

    private bool seeCatSpirit;
    private GameObject catSpirit;

    private bool seeDeadCat;
    private GameObject deadCat;
    private GameObject grabbedDeadCat;
    


    public TextMeshProUGUI indication;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(grabbedCat == null && grabbedDeadCat == null)//si on voit un chat et qu'on � pas de chat dans les bras

            if (seeCat)
            {
                //on peut chopper le chat qu'on voit & s'il n'est pas dans la SafeZone
                CatController catController = seenCat.GetComponent<CatController>();
                
                if (!catController.isInSafeZone)
                {
                    indication.text = "Appuyez sur E pour saisir le chat";
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

                    }
                }else //si le chat est dans la SafeZone
                {
                    indication.text = "Le chat est dans la SafeZone. Bravo !";
                    indication.gameObject.SetActive(true);
                }
   
            }else if (seeCatSpirit)
            {
                Talker catTalker = catSpirit.GetComponent<Talker>();

                indication.text = "Appuyez sur C pour discuter avec l'esprit";
                indication.gameObject.SetActive(true);

                if (Input.GetKeyDown("c") && !catTalker.isInDialogue())
                {
                    catTalker.TriggerDialogue();
                }

            }
            else if (seeDeadCat)
            {
                DeadCatController catController = deadCat.GetComponent<DeadCatController>();

                if (!catController.isInSafeZone)
                {
                    indication.text = "Appuyez sur E pour porter le corps du chat";
                    indication.gameObject.SetActive(true);
                    if (Input.GetKeyDown("e"))
                    { 
                        grabbedDeadCat = deadCat;
                        grabbedDeadCat.GetComponent<Rigidbody>().freezeRotation = true;
                        grabbedDeadCat.GetComponent<Rigidbody>().detectCollisions = false;
                        catController.grabCat();
                        indication.gameObject.SetActive(false);
                    }
                }
                else //si le chat est dans la SafeZone
                {
                    indication.text = "Le chat est dans la SafeZone.";
                    indication.gameObject.SetActive(true);
                }
            }
            else
            {
                indication.gameObject.SetActive(false);
            }
        else //si on a un chat dans les bras
        {

            if (grabbedCat)
            {
                grabbedCat.transform.position = transform.position + transform.forward;
                Vector3 directionToPlayer = (transform.position - grabbedCat.transform.position).normalized;
                grabbedCat.transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

            }else if (grabbedDeadCat)
            {
                grabbedDeadCat.transform.position = transform.position + transform.forward/3;
                grabbedDeadCat.transform.rotation = transform.rotation * Quaternion.Euler(0,90,90);
            }
           

            if (Input.GetKeyDown("e"))
            {
                if (grabbedCat)
                {
                    putDownCat();
                }else if (grabbedDeadCat)
                {
                    grabbedDeadCat.GetComponent<Rigidbody>().detectCollisions = true;
                    grabbedDeadCat.GetComponent<DeadCatController>().releaseCat();
                    grabbedDeadCat = null;
                    
                }
                
                
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
        if (other.gameObject.CompareTag("CatSpirit"))
        {
            seeCatSpirit = true;
            catSpirit = other.gameObject;
        }
        if (other.gameObject.CompareTag("DeadCat"))
        {
            seeDeadCat = true;
            deadCat = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grabable"))
        {
            seeCat = false;
            seenCat = null;
        }
        if (other.gameObject.CompareTag("CatSpirit"))
        {
            seeCatSpirit = false;
            catSpirit = null;
        }
        if (other.gameObject.CompareTag("DeadCat"))
        {
            seeDeadCat = false;
            deadCat = null;
        }
    }

    public void putDownCat()
    {
        grabbedCat.GetComponent<Animator>().SetBool("Walk", true);
        grabbedCat.GetComponent<Animator>().SetBool("Sit", false);
        grabbedCat = null;
    }
}
