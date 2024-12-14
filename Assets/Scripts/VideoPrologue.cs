using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPrologue : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Obtenir le composant VideoPlayer
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("Aucun VideoPlayer trouvé sur cet objet !");
            return;
        }

        // Ajouter l'événement pour détecter la fin de la vidéo
        videoPlayer.loopPointReached += OnVideoEnd;

        Debug.Log("Lecture de la vidéo de prologue");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Log
        Debug.Log("Fin de la vidéo de prologue");

        // Changer de scène
        SceneManager.LoadScene("Realistic");
    }
}
