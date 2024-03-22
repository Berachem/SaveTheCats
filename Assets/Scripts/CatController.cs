using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public bool isInSafeZone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInSafeZone)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
