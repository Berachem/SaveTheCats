using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FootstepTrigger : MonoBehaviour
{
    public AudioSource footstepAudio;
    public ActionBasedContinuousMoveProvider moveProvider; // ou DynamicMoveProvider si tu l'as
    public float stepInterval = 0.5f;

    private float stepTimer = 0f;
    private bool wasMoving = false;

    void Update()
    {
        if (moveProvider == null || footstepAudio == null) return;

        Vector2 input = moveProvider.leftHandMoveAction.action.ReadValue<Vector2>();
        bool isMoving = input.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                footstepAudio.Stop();
                footstepAudio.Play();
                stepTimer = stepInterval;
            }
        }

        if (!isMoving && wasMoving)
        {
            footstepAudio.Stop();
            stepTimer = 0f;
        }

        wasMoving = isMoving;
    }
}
