using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class CheckpointSaveTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer _interactSprite;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject saveScreen;
    [SerializeField] private float rearmDelay = 1.5f; //thought of everything
    [SerializeField] public FloatVariable checkpointInteractableRange;

    private float nextAllowedTime;
    private bool screenActive;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        screenActive = false;
        nextAllowedTime = 0f;
        if (saveScreen != null)
        {
            saveScreen.SetActive(false);
        }
    }

    private void Update()
    {
        // um um if interact was pressed this frame and um um the flag is near err.....oh no!
        if (InputSystem.actions.FindAction("Interact").WasPressedThisFrame() &&
            IsWithinInteractDistance() &&
            Time.time >= nextAllowedTime &&
            !screenActive)
        {
            Interact();
        }

        if (!IsWithinInteractDistance())
        {
            if (_interactSprite != null)
            {
                _interactSprite.enabled = false;
            }

            if (screenActive)
            {
                CloseSaveScreen();
            }
        }
        else if (IsWithinInteractDistance() && Time.time >= nextAllowedTime && !screenActive)
        {
            if (_interactSprite != null)
            {
                _interactSprite.enabled = true;
            }
        }
    }

    public void Interact()
    {
        if (saveScreen == null)
            return;

        saveScreen.SetActive(true);
        screenActive = true;

        if (_interactSprite != null)
        {
            _interactSprite.enabled = false;
        }
    }

    public void CloseSaveScreen()
    {
        saveScreen.SetActive(false);
        screenActive = false;
        nextAllowedTime = Time.time + rearmDelay;
    }

    private bool IsWithinInteractDistance()
    {
        if (_playerTransform == null || checkpointInteractableRange == null)
            return false;

        return Vector2.Distance(_playerTransform.position, transform.position) < checkpointInteractableRange.Value;
    }
}