using UnityEngine;

public class CatMiaou : MonoBehaviour
{
    public AudioClip[] miaouClips;
    public float rayonAudible = 10f;
    public float minDelay = 5f;
    public float maxDelay = 20f;

    private AudioSource audioSource;
    private Transform player;
    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = Camera.main.transform; // XR camera
        ResetTimer();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > rayonAudible)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (miaouClips.Length > 0 && !audioSource.isPlaying)
            {
                int index = Random.Range(0, miaouClips.Length);
                audioSource.clip = miaouClips[index];
                audioSource.Play();
            }
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }
}
