using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FloatGameEvent alterTimeGameEvent;
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private GameState gameState;
    [SerializeField] private TextMeshProUGUI completionPercentText;
    private bool isPaused;

    private void Awake()
    {
        //set collectibles to 0
        gameState.numCollectibles = 0;
    }
    // Update is called once per frame
    void Update()
    {
        gameState.currentScene = SceneManager.GetActiveScene().name;
        gameState.currentCameraName = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Name;
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
        string completionPercentStringRounded = Mathf.Round(gameState.calcPercent()).ToString();
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
