using UnityEngine;

[CreateAssetMenu(fileName = "UnlockBoolEffect", menuName = "Pickups/Unlock Bool")]
public class UnlockBoolEffectSO : PickupEffectSO
{
    [SerializeField] private BoolVariable flag;
    [SerializeField] private bool setTo = true;

    public override void Apply(GameObject interactor)
    {
        if (flag != null) flag.Value = setTo;
    }
}