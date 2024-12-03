using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoPrologue : MonoBehaviour
{
    public Canvas[] canvases; // Référence à tous les Canvas à désactiver
    private Dictionary<Canvas, bool> canvasStates = new Dictionary<Canvas, bool>(); // Stocke les états initiaux des Canvas
    private VideoPlayer videoPlayer;
    private AudioSource[] allAudioSources; // Pour stocker toutes les Audio Sources

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Enregistrer l'état initial de chaque Canvas
        foreach (Canvas canvas in canvases)
        {
            canvasStates[canvas] = canvas.gameObject.activeSelf; // Sauvegarde l'état actif/inactif
            canvas.gameObject.SetActive(false); // Désactive tous les Canvas
        }

        // Récupérer toutes les Audio Sources dans la scène
        allAudioSources = FindObjectsOfType<AudioSource>();

        // Couper tous les sons sauf celui de la vidéo (si elle a un son)
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (videoPlayer.GetTargetAudioSource(0) != null && audioSource == videoPlayer.GetTargetAudioSource(0))
            {
                continue; // Ne coupe pas l'Audio Source attachée à la vidéo
            }
            audioSource.mute = true; // Mute tous les autres sons
        }

        // Écouter l'événement de fin de vidéo
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Restaurer l'état initial des Canvas
        foreach (var entry in canvasStates)
        {
            entry.Key.gameObject.SetActive(entry.Value); // Restaure l'état actif/inactif
        }

        // Réactiver tous les sons
        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = false;
        }

        // Désactiver ou détruire l'objet Video Player
        gameObject.SetActive(false); // Désactive l'objet contenant le Video Player
        // Destroy(gameObject); // Si vous préférez le détruire complètement
    }
}
