using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool isInSafeZone = false;
    public AudioClip[] meowSounds; // Tableau des sons de miaulement
    public AudioClip purrSound; // Le son de ronronnement
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Walk");
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(HandleCatSounds());
    }

    // Gère le miaulement et le ronronnement à des intervalles aléatoires
    IEnumerator HandleCatSounds()
    {
        while (true) // Continue indéfiniment
        {
            yield return new WaitForSeconds(Random.Range(25, 50)); // Attend un intervalle aléatoire

            if (isInSafeZone)
            {
                // Si le chat est dans la safe zone, il ronronne
                Purr();
            }
            else
            {
                // Sinon, le chat miaule avec un des sons de miaulement aléatoires
                Meow();
            }
        }
    }

    public void Meow()
    {
        if (audioSource != null && meowSounds.Length > 0)
        {
            // Sélectionne un son de miaulement aléatoire
            AudioClip randomMeow = meowSounds[Random.Range(0, meowSounds.Length)];
            audioSource.clip = randomMeow;
            audioSource.Play(); // Joue le son de miaulement sélectionné
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
