using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public bool isInSafeZone = false;
    public AudioClip[] meowSounds; // Tableau des sons de miaulement
    public AudioClip purrSound; // Le son de ronronnement
    private AudioSource audioSource;

    public string catName = "Minou";
    public int age = 3;

    // histoire triste du chat
    public string story = "Minou est un chat errant qui a été abandonné par sa famille. Il a subi de la maltraitance, a été blessé et était affamé. Minou n'a pas eu la chance de survivre à la rue et est mort de faim.";
    public string storyDate = "15/03/2021";

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
