using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool isInSafeZone = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Walk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
