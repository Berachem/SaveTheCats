using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SafeZoneController : MonoBehaviour
{
    public TextMeshProUGUI indication; // Texte pour les indications des chats
    private int catsRemaining;
    public InterfaceController userInterface;

    public AudioClip victorySound; // AudioClip pour le son de victoire
    private AudioSource audioSource;




    private void Start()
    {
        // Initialiser catsRemaining avec le nombre de chats dans la scène
        catsRemaining = GameObject.FindGameObjectsWithTag("DeadCat").Length;
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            // Si aucun AudioSource n'est trouvé, en ajoute un dynamiquement
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
        }else if (other.CompareTag("DeadCat"))
        {
            DeadCatController controller = other.GetComponent<DeadCatController>();
            controller.enterSafeZone();
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
        else if (other.CompareTag("DeadCat"))
        {
            DeadCatController controller = other.GetComponent<DeadCatController>();
            controller.exitSafeZone();
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
            userInterface.StopTimer(); // Arrête le timer
            //PlayVictorySound();

            Debug.Log("Victoire !");

            // On change de scène vers l'épilogue
            SceneManager.LoadScene("Epilogue");


        }
    }


    private void PlayVictorySound()
    {
        if (victorySound != null && audioSource != null)
        {
            audioSource.clip = victorySound;
            audioSource.volume = 1f;
            audioSource.Play(); // Joue le son de victoire
            Debug.Log("Lecture du son Victoire !");
        }
    }


}
