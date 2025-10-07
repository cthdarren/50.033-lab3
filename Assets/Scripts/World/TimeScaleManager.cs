using System.Collections;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public float defaultTimeScale = 1f;
    public void AlterTimeScaleForSecondsLerped(float timescale, float duration)
    {
        Time.timeScale = timescale;
        StartCoroutine(WaitForDurationBeforeLerpingToDefault(timescale, duration));
    }

    public void AlterTimeScaleForSeconds(float timescale, float duration)
    {
        Time.timeScale = timescale;
        StartCoroutine(WaitForDurationBeforeReturningToDefault(duration));
    }

    public IEnumerator WaitForDurationBeforeReturningToDefault(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        ResetTimeScaleToDefault();
    }

    public IEnumerator WaitForDurationBeforeLerpingToDefault(float timescale, float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            AlterTimeScale(Mathf.Lerp(timescale, defaultTimeScale, elapsed / duration));
            yield return null;
        }
        ResetTimeScaleToDefault();
    }

    public void ResetTimeScaleToDefault(){
        AlterTimeScale(defaultTimeScale);
    }
    public void LerpToDefault(){
        AlterTimeScale(defaultTimeScale);
    }

    public void AlterTimeScale(float timescale){
        Time.timeScale = timescale;
    }
}
