using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class SafeZoneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabable"))
        {
            CatController catController = other.GetComponent<CatController>();
            catController.isInSafeZone = true;
            catController.GetComponent<NavMeshAgent>().enabled = false;
            catController.GetComponent<WanderingAI>().enabled = false;
            catController.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabable"))
        {
            CatController catController = other.GetComponent<CatController>();
            catController.isInSafeZone = false;
            catController.GetComponent<NavMeshAgent>().enabled = true;
            catController.GetComponent<WanderingAI>().enabled = true;
            catController.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
