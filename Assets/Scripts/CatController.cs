using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool isInSafeZone = false;
    public AudioClip meowSound; // Le son de miaulement
    public AudioClip purrSound; // Le son de ronronnement
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Walk");
        audioSource = gameObject.AddComponent<AudioSource>();

        // Commence à vérifier le comportement de miaulement et de ronronnement
        StartCoroutine(HandleCatSounds());
    }

    // Gère le miaulement et le ronronnement à des intervalles aléatoires
    IEnumerator HandleCatSounds()
    {
        while (true) // Continue indéfiniment
        {
            yield return new WaitForSeconds(Random.Range(10, 20)); // Attend un intervalle aléatoire entre 30 et 60 secondes

            if (isInSafeZone)
            {
                // Si le chat est dans la safe zone, il ronronne
                Purr();
            }
            else
            {
                // Sinon, le chat miaule
                Meow();
            }
        }
    }

    public void Meow()
    {
        if (audioSource != null && meowSound != null)
        {
            audioSource.clip = meowSound;
            audioSource.Play(); // Joue le son de miaulement
        }
    }

    public void Purr()
    {
        if (audioSource != null && purrSound != null)
        {
            audioSource.clip = purrSound;
            audioSource.Play(); // Joue le son de ronronnement
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
