using UnityEngine;

public class AddAmbienceAudioSourceOnStart : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string audioClipName;
    [SerializeField] private AddAudioSourceGameEvent addAudioSourceGameEvent;

    private void Start()
    {
        addAudioSourceGameEvent.Raise(audioClipName, audioSource);
    }
}
