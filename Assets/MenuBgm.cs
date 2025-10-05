using UnityEngine;

public class MenuBgm : MonoBehaviour
{
    [SerializeField] public AudioSource[] audioSources;
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip loopClip;
    private AudioClip currentClip;
    [SerializeField] private int sourceToggle = 0;
    private double nextClipStartTime;
    private double clipDuration;

    // Do NOT call in awake
    private void Start()
    {
        nextClipStartTime = AudioSettings.dspTime + 1;
        SetCurrentClip(introClip);
        ScheduleClipToPlayOnNextClipStartTime();
        SetCurrentClip(loopClip);
    }

    private void Update()
    {
        if (AudioSettings.dspTime > nextClipStartTime - 1) { 
            ScheduleClipToPlayOnNextClipStartTime();
        }
    }

    private void ScheduleClipToPlayOnNextClipStartTime()
    {
        audioSources[sourceToggle].clip = currentClip;
        audioSources[sourceToggle].PlayScheduled(nextClipStartTime); 

        clipDuration = (double)currentClip.samples / currentClip.frequency;
        nextClipStartTime += clipDuration;

        sourceToggle = 1 - sourceToggle; // (1-0) = 1,  (1-1) = 0;
    }

    private void SetCurrentClip(AudioClip newClip)
    {
        currentClip = newClip; 
    }
}
