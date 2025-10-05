using System.Collections;
using UnityEngine;

public class MenuBgm : MonoBehaviour
{
    [SerializeField] public AudioSource[] audioSources;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip loopClip;
    private AudioClip currentClip;
    [SerializeField] private int audioToggle = 0;
    private double nextClipStartTime;
    private double clipDuration;

    // Do NOT call in awake
    private void Start()
    {
        nextClipStartTime = AudioSettings.dspTime + 1; // t + 1
        SetCurrentClip(introClip);
        PlayScheduledClip();
        SetCurrentClip(loopClip);
    }

    private void Update()
    {
        // 1 second before current clip ends

        Debug.Log(AudioSettings.dspTime);
        Debug.Log(nextClipStartTime);
        if (AudioSettings.dspTime > nextClipStartTime - 1) { 
            PlayScheduledClip();
        }
    }

    private void PlayScheduledClip()
    {
        audioSources[audioToggle].clip = currentClip;
        // schedule this clip to play at the next timestamp 
        audioSources[audioToggle].PlayScheduled(nextClipStartTime); 

        // calcualte the duration of the newly scheduled clip
        clipDuration = (double)currentClip.samples / currentClip.frequency;
        nextClipStartTime += clipDuration;

        audioToggle = 1 - audioToggle; // (1-0) = 1,  (1-1) = 0;
    }

    private void SetCurrentClip(AudioClip newClip)
    {
        currentClip = newClip; 
    }
}
