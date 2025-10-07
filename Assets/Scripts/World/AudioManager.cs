using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio(string soundEffectName)
    {
        AudioClip audioClip = soundLibrary.GetClipFromName(soundEffectName);
        audioSource.PlayOneShot(audioClip);
    }
}
