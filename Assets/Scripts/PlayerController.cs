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
        //si on voit un chat et qu'on à pas de chat dans les bras
        if(grabbedCat == null)

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
                        // Vérifie si le chat peut être saisi

                        grabbedCat = seenCat;
                        grabbedCat.GetComponent<Rigidbody>().freezeRotation = true;
                        indication.gameObject.SetActive(false);

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
        grabbedCat.transform.rotation = Quaternion.identity;

        // Activez la gravité si elle était désactivée
        Rigidbody catRigidbody = grabbedCat.GetComponent<Rigidbody>();
        catRigidbody.freezeRotation = false;
        catRigidbody.useGravity = true; // Assurez-vous que cette ligne est nécessaire selon votre configuration

        grabbedCat = null;
    }
}
