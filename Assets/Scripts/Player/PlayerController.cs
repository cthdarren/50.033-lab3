using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerInput playerInput;
    [SerializeField] private FloatVariable dashCooldownTimer;
    public void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if (playerInput.pauseInput.WasPressedThisFrame())
        {
            //Pause the game
        }
        playerCombat.HandleCombat();
        playerMovement.HandleMovement();
    }

    // this should be in PlayerDash
    public void FixedUpdate()
    {
        dashCooldownTimer.Value -= Time.deltaTime;
    }
}
