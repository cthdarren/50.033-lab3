using UnityEngine;

public class RemoveAmbienceAudioSourceByName: MonoBehaviour
{
    [SerializeField] private string audioClipName;
    [SerializeField] private GameEvent<string> removeAudioSourceGameEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            removeAudioSourceGameEvent.Raise(audioClipName);
        }
    }
}
