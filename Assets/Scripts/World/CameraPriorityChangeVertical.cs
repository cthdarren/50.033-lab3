using UnityEngine;
using Unity.Cinemachine;

public class CameraPriorityChangeVertical: MonoBehaviour
{
    public CinemachineCamera upCamera;
    public CinemachineCamera downCamera;
    private BoxCollider2D triggerZone;
    private void Awake()
    {
        triggerZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.y > triggerZone.bounds.center.y)
            {
                upCamera.Priority = 10;
                downCamera.Priority = 5;
            }
            else
            {
                downCamera.Priority = 10;
                upCamera.Priority = 5;
            }
        }
        
    }
}
