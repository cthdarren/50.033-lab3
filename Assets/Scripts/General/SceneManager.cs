using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public void ChangeCurrentScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void PlayAudio(string soundEffectName)
    {
        Debug.Log("Playing audio clip" + soundEffectName);
    }
}


public class SceneenenManager : MonoBehaviour

{
    [SerializeField] private SoundLibrary soundLibrary;
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio(string soundEffectName)
    {
        Debug.Log("Playing audio clip" + soundEffectName);
        AudioClip audioClip = soundLibrary.GetClipFromName(soundEffectName);
        audioSource.PlayOneShot(audioClip);
    }
}
