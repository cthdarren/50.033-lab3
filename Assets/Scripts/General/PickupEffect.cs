using UnityEngine;

public abstract class PickupEffectSO : ScriptableObject
{
    public abstract void Apply(GameObject interactor);
}