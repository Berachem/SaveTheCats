using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEpilogue : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Obtenir le composant VideoPlayer
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("Aucun VideoPlayer trouv� sur cet objet !");
            return;
        }

        // Ajouter l'�v�nement pour d�tecter la fin de la vid�o
        videoPlayer.loopPointReached += OnVideoEnd;

        Debug.Log("Lecture de la vid�o de l'�pilogue");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Log
        Debug.Log("Fin de la vid�o de l'�pilogue");

        // Changer de sc�ne
        SceneManager.LoadScene("StartMenu");
    }
}
