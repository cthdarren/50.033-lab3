using UnityEngine;
using Unity.Cinemachine;

public class CameraPriorityChangeHorizontal: MonoBehaviour
{
    public CinemachineCamera leftCamera;
    public CinemachineCamera rightCamera;
    private BoxCollider2D triggerZone;
    private void Awake()
    {
        triggerZone = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x > triggerZone.bounds.center.x)
            {
                rightCamera.Priority = 10;
                leftCamera.Priority = 5;
            }
            else
            {
                leftCamera.Priority = 10;
                rightCamera.Priority = 5;
            }
        }
        
    }
}
