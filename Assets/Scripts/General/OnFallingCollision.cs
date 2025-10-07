using Unity.Cinemachine;
using UnityEngine;

public class OnFallingCollision : MonoBehaviour
{
    [SerializeField] private Collider2D fallCollider;
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private PlayAudioClipGameEvent playAudioClipGameEvent;
    [SerializeField] private SimpleGameEvent onLandGameEvent;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            impulseSource.GenerateImpulseWithForce(0.3f);
            fallCollider.enabled = false;
            playAudioClipGameEvent.Raise("HeavyLanding");
            onLandGameEvent.Raise();
        }
    }
}
