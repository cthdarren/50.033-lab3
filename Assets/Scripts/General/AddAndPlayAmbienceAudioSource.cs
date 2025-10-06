using UnityEngine;

public class AddAndPlayAmbienceAudioSource: MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string audioClipName;
    [SerializeField] private AddAudioSourceGameEvent addAudioSourceGameEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            addAudioSourceGameEvent.Raise(audioClipName, audioSource);
    }
}
