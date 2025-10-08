using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatGameEvent alterTimeGameEvent;
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private CompletionPercentSO completionPercentSO;
    [SerializeField] private TextMeshProUGUI completionPercentText;
    private bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Pause").WasPressedThisFrame())
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        string completionPercentStringRounded = Mathf.Round(completionPercentSO.calcPercent()).ToString();
        completionPercentText.text = $"Completion: {completionPercentStringRounded}%";
        alterTimeGameEvent.Raise(0f);
        pauseMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        alterTimeGameEvent.Raise(1f);
        pauseMenu.gameObject.SetActive(false);
    }
}
