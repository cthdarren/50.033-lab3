using System.Collections;
using UnityEngine;

public class DamageFlash: MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.25f;


    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;
    private Coroutine damageFlashCoroutine;

    public void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        materials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    public void CallDamageFlash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        float currentFlashAmount = 0f;
        float elapsed = 0f;

        while (elapsed < flashTime)
        {
            elapsed += Time.unscaledDeltaTime;
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsed / flashTime));
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }

    private void SetFlashAmount(float amount)
    {
        for (int i = 0; i < materials.Length; i++) {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }
    private void SetFlashColor()
    {
        for (int i = 0; i < materials.Length; i++) {
            materials[i].SetColor("_FlashColor", flashColor);
        }
    }
}
