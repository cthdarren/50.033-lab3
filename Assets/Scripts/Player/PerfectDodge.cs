using System.Collections;
using UnityEngine;

public class PerfectDodge : MonoBehaviour
{
    [SerializeField] private Collider2D perfectDodgeHitbox;
    [SerializeField] private float perfectDodgeWindow;
    [SerializeField] private float perfectDodgeTimeScale;
    [SerializeField] private float slowMoDuration;
    [SerializeField] private float timeStopDuration;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 vectorToFollow;
    [SerializeField] private FloatFloatGameEvent alterTimeScaleGameEvent;
    [SerializeField] private FloatGameEvent alterTimeScalePermGameEvent;
    [SerializeField] private PlayAudioClipGameEvent gameEvent;
    [SerializeField] private Canvas slowMoOverlay;

    public void Start()
    {
        vectorToFollow = playerTransform.position;
    }

    public void Update()
    {
        if (!perfectDodgeHitbox.enabled)
            vectorToFollow = playerTransform.position;
        transform.position = vectorToFollow;
    }
    public void DropHitbox()
    {
        perfectDodgeHitbox.enabled = true;
        vectorToFollow = transform.position;
        StartCoroutine(DeactivatePerfectDodgeHitbox());
    }

    public IEnumerator DeactivatePerfectDodgeHitbox()
    {
        yield return new WaitForSecondsRealtime(perfectDodgeWindow);
        vectorToFollow = playerTransform.position;
        slowMoOverlay.enabled = false;
        perfectDodgeHitbox.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttackHitbox")){
            gameEvent.Raise("SlowMo");
            slowMoOverlay.enabled = true;
            alterTimeScalePermGameEvent.Raise(0f);
            StartCoroutine(SlowMo());
        }
    }

    public IEnumerator SlowMo()
    {
        yield return new WaitForSecondsRealtime(timeStopDuration);
        alterTimeScaleGameEvent.Raise(perfectDodgeTimeScale, slowMoDuration);
    }
}
