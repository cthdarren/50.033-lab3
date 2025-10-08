using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameEvent collectedSoul;
    public Transform angelTransform;
    public float speed;
    private bool collected = false;

    private void Update()
    {
        if (collected)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, angelTransform.position, step);
            if (Vector3.Distance(transform.position, angelTransform.position) < 0.01f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collectedSoul.Raise();
            collected = true;
        }
    }
}
