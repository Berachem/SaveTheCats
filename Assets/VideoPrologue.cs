using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoPrologue : MonoBehaviour
{
    public Canvas[] canvases; // R�f�rence � tous les Canvas � d�sactiver
    private Dictionary<Canvas, bool> canvasStates = new Dictionary<Canvas, bool>(); // Stocke les �tats initiaux des Canvas
    private VideoPlayer videoPlayer;
    private AudioSource[] allAudioSources; // Pour stocker toutes les Audio Sources

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Enregistrer l'�tat initial de chaque Canvas
        foreach (Canvas canvas in canvases)
        {
            canvasStates[canvas] = canvas.gameObject.activeSelf; // Sauvegarde l'�tat actif/inactif
            canvas.gameObject.SetActive(false); // D�sactive tous les Canvas
        }

        // R�cup�rer toutes les Audio Sources dans la sc�ne
        allAudioSources = FindObjectsOfType<AudioSource>();

        // Couper tous les sons sauf celui de la vid�o (si elle a un son)
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (videoPlayer.GetTargetAudioSource(0) != null && audioSource == videoPlayer.GetTargetAudioSource(0))
            {
                continue; // Ne coupe pas l'Audio Source attach�e � la vid�o
            }
            audioSource.mute = true; // Mute tous les autres sons
        }

        // �couter l'�v�nement de fin de vid�o
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Restaurer l'�tat initial des Canvas
        foreach (var entry in canvasStates)
        {
            entry.Key.gameObject.SetActive(entry.Value); // Restaure l'�tat actif/inactif
        }

        // R�activer tous les sons
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = false;
        }

        // D�sactiver ou d�truire l'objet Video Player
        gameObject.SetActive(false); // D�sactive l'objet contenant le Video Player
        // Destroy(gameObject); // Si vous pr�f�rez le d�truire compl�tement
    }
}
