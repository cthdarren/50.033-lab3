using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio(string soundEffectName)
    {
        Debug.Log("Playing audio clip" + soundEffectName);
        AudioClip audioClip = soundLibrary.GetClipFromName(soundEffectName);
        audioSource.PlayOneShot(audioClip);
    }

    public void Bruh(string soundEffectName)
    {
        Debug.Log("Playing audio clip" + soundEffectName);
    }
}
