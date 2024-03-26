using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SafeZoneController : MonoBehaviour
{
    public TextMeshProUGUI indication; // Texte pour les indications des chats
    private int catsRemaining;
    public InterfaceController userInterface;


    private void Start()
    {
        // Initialiser catsRemaining avec le nombre de chats dans la scène
        catsRemaining = GameObject.FindGameObjectsWithTag("Grabable").Length;
        UpdateIndicationText();
    }

    private void Update()
    {
        UpdateIndicationText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabable"))
        {
            CatController catController = other.GetComponent<CatController>();
            catController.isInSafeZone = true;
            catController.GetComponent<NavMeshAgent>().enabled = false;
            catController.GetComponent<WanderingAI>().enabled = false;
            catController.GetComponent<Rigidbody>().isKinematic = false;
            catController.GetComponent<Animator>().SetBool("Walk", false);
            catController.GetComponent<Animator>().SetBool("Sit", true);

            // Décrémente catsRemaining et met à jour le texte
            catsRemaining--;
            UpdateIndicationText();
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
            catController.GetComponent<Animator>().SetBool("Walk", true);
            catController.GetComponent<Animator>().SetBool("Sit", false);

            // Incrémente catsRemaining et met à jour le texte
            catsRemaining++;
            UpdateIndicationText();
        }
    }

    private void UpdateIndicationText()
    {
        if (catsRemaining > 0)
        {
            // Texte avec X en rouge
            indication.text = $"Encore <color=#FF0000>{catsRemaining}</color> chats à sauver !";
        }
        else
        {
            // Texte "Victoire !" en vert
            indication.text = "<color=#00FF00>Victoire !</color>";
            userInterface.StopTimer(); // Arrête le timer quand il n'y a plus de chats à sauver
        }
    }


}
