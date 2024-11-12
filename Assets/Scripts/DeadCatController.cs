using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCatController : MonoBehaviour
{
    public bool isInSafeZone = false;
    public GameObject catSpirit;


    public void grabCat()
    {
        catSpirit.SetActive(false);
    }

    public void releaseCat()
    { 
        catSpirit.SetActive(true);
        catSpirit.transform.position = transform.position;
    }

    public void enterSafeZone()
    {
        isInSafeZone = true;
        catSpirit.SetActive(false);
    }

    public void exitSafeZone()
    {
        isInSafeZone = false;
        catSpirit.SetActive(true);
    }
}
