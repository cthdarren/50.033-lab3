using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AudioSourceLibrary: MonoBehaviour
{
    private Dictionary<string,AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private float fadeTime = 1;

    public void AddAudioSourceAndPlay(string name, AudioSource audioSource)
    {
        if (audioSources.ContainsKey(name)) return;
        Debug.Log($"Adding {name} to dictionary");
        Debug.Log($"with {audioSource} audioSource");
        audioSources.Add(name, audioSource);
        Debug.Log($"Dictionary now contains: {audioSources.ToString()}");
        audioSources[name].volume = 0;
        audioSources[name].Play();
        StartCoroutine(FadeIn(name));
    }

    public IEnumerator FadeIn(string name)
    {
        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float t = time / fadeTime;

            audioSources[name].volume = Mathf.Lerp(0, 1f, t);
            yield return null;
        }
        audioSources[name].volume = 1f;
    }

    public void StopAndRemoveAudioSource(string name)
    {
        if (audioSources.ContainsKey(name))
        {
            StartCoroutine(FadeOutAndStop(name));
        }
    }
    public IEnumerator FadeOutAndStop(string name)
    {
        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float t = time / fadeTime;

            audioSources[name].volume = Mathf.Lerp(1f, 0, t);
            yield return null;
        }
        audioSources[name].Stop();
        audioSources.Remove(name);
    }

}
