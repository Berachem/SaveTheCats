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
            Debug.LogError("Aucun VideoPlayer trouv� sur cet objet !");
            return;
        }

        // Ajouter l'�v�nement pour d�tecter la fin de la vid�o
        videoPlayer.loopPointReached += OnVideoEnd;

        Debug.Log("Lecture de la vid�o de prologue");
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Log
        Debug.Log("Fin de la vid�o de prologue");

        // Changer de sc�ne
        SceneManager.LoadScene("Realistic");
    }
}
