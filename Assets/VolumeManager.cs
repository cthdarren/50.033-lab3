using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string volumeParamName;
    public void ChangeVolume(float volume)
    {
        Debug.Log("changing volume to: " + volume.ToString());
        audioMixer.SetFloat(volumeParamName, Mathf.Min(volume, 0));
    }
}
