using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] private BoolVariable isWaiting; 
    [SerializeField] private FloatVariable hitStopDuration; 
    [SerializeField] private FloatFloatGameEvent alterTimeGameEvent; 

    public void OnPlayerAttackEnemy()
    {
        Debug.Log("Enemy attacked, stopping for " + hitStopDuration.Value.ToString());
        alterTimeGameEvent.Raise(0f, hitStopDuration.Value);
        //Stop(hitStopDuration.Value);
    }

    public void Stop(float duration_seconds)
    {
    }
}
