using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] private BoolVariable isWaiting; 
    [SerializeField] private FloatVariable hitStopDuration; 

    public void OnPlayerAttackEnemy()
    {
        Stop(hitStopDuration.Value);
    }

    public void Stop(float duration_seconds)
    {
        if (isWaiting) return;
        StartCoroutine(WaitAsync(duration_seconds));
    }

    public IEnumerator WaitAsync(float duration_seconds)
    {
        isWaiting.Value = true;
        Time.timeScale = 0f;
        yield return null;
        yield return new WaitForSecondsRealtime(duration_seconds);
        Time.timeScale = 1f;
        isWaiting.Value = false ;
    }
}
