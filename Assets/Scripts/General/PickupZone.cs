using System.Collections.Generic;
using UnityEngine;
public class PickupZone : MonoBehaviour, IInteractable
{
    [SerializeField] private List<PickupEffectSO> effects = new();
    [SerializeField] private bool autoPickupOnPlayerEnter = true;
    [SerializeField] private bool destroyOnPickup = true;

    private GameObject lastInteractor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!autoPickupOnPlayerEnter) return;
        if (!other.CompareTag("Player")) return;
        lastInteractor = other.gameObject;
        Interact();
    }

    public void Interact()
    {
        var interactor = lastInteractor != null ? lastInteractor : gameObject;
        ApplyEffects(interactor);
        if (destroyOnPickup) Destroy(gameObject);
        lastInteractor  = null;
    }

    private void ApplyEffects(GameObject interactor)
    {
        if (effects == null) return;
        foreach (var e in effects)
            if (e != null) e.Apply(interactor);
    }
}