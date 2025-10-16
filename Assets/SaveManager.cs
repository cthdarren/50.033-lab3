using System;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private BoolVariable doubleJumpUnlocked;
    [SerializeField] private FloatVariable playerHp;
    [SerializeField] private ChangeSceneGameEvent changeSceneEvent;
    [SerializeField] private SaveGameData? loadedGameData;
    private bool triggerLoadGame = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ApplyPlayerStateChanges;
    }

    public void SaveGame(int slot) 
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerTransformVector = player.transform.position;
        float[] playerPositionFloatArr = 
            new float[3]{
                playerTransformVector.x,
                playerTransformVector.y,
                playerTransformVector.z
            };

        SaveGameData saveData = new SaveGameData(
                playerPositionFloatArr,
                gameState.Serialize(),
                playerState.Serialize(),
                doubleJumpUnlocked.Value,
                playerHp.Value,
                gameState.saveStartTime
            );

        WriteToSaveFile(saveData, slot);
    }
    public void LoadGame(int slot) 
    {
        triggerLoadGame = true;
        loadedGameData = ReadFromSaveFile(slot);
        if (!loadedGameData.HasValue) return;
        doubleJumpUnlocked.Value = loadedGameData.Value.doubleJumpUnlocked;
        playerHp.Value = loadedGameData.Value.playerHp;
        LoadGameState(loadedGameData.Value.gameState);
        changeSceneEvent.Raise(gameState.currentScene);
    }

    public void ApplyPlayerStateChanges(Scene scene, LoadSceneMode mode)
    {
        if (!triggerLoadGame) return;

        LoadPlayerState(loadedGameData.Value.playerState);

        GameObject activeCamera = GameObject.Find(gameState.currentCameraName);
        activeCamera.GetComponent<CinemachineCamera>().Priority = 100;
        GameObject player = GameObject.Find("Player");

        float[] savedPlayerPosition = loadedGameData.Value.playerPosition;
        player.transform.position = new Vector3(
                savedPlayerPosition[0],
                savedPlayerPosition[1],
                savedPlayerPosition[2]
            );

        triggerLoadGame = false;
    }

    private bool WriteToSaveFile(SaveGameData data, int slot)
    {
        string json = JsonUtility.ToJson(data, true);
        string savePath = Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");

        File.WriteAllText(savePath, json);
        Debug.Log("Wrote to savefile in slot" + slot);
        Debug.Log(json);
        return File.Exists(savePath);
    }

    private SaveGameData? ReadFromSaveFile(int slot)
    {
        string savePath = Path.Combine(Application.persistentDataPath, $"save_slot_{slot}.json");

        if (File.Exists(savePath)) 
        {
            string json = File.ReadAllText(savePath);
            Debug.Log("Read fromsavefile in slot" + slot);
            Debug.Log(json);
            return JsonUtility.FromJson<SaveGameData>(json);
        }
        else
        {
            return null;
        }
    }

    private void LoadGameState(GameStateSerialized serialized)
    {
        gameState.currentScene = serialized.currentScene;
        gameState.numCollectibles = serialized.numCollectibles;
        gameState.maxCollectibles = serialized.maxCollectibles;
        gameState.bossDefeated = serialized.bossDefeated;
        gameState.saveStartTime = serialized.saveStartTime;
    }

    private void LoadPlayerState(PlayerStateSerialized serialized)
    {
        playerState.isAggroed = serialized.isAggroed;
        playerState.isJumping = serialized.isJumping;
        playerState.isDashing = serialized.isDashing;
        playerState.isInvincible = serialized.isInvincible;
        playerState.isMovementDisabled = serialized.isMovementDisabled;
        playerState.isGrounded = serialized.isGrounded;
    }
}

[Serializable]
public struct SaveGameData
{
    public float[] playerPosition;
    public GameStateSerialized gameState;
    public bool doubleJumpUnlocked;
    public float playerHp;
    public PlayerStateSerialized playerState;
    public DateTime startDateTime;

    public SaveGameData(
        float[] position,
        GameStateSerialized gameState,
        PlayerStateSerialized playerState,
        bool doubleJumpUnlocked,
        float playerHp,
        DateTime startDateTime
        )
    {
        playerPosition = position;
        this.doubleJumpUnlocked = doubleJumpUnlocked;
        this.playerHp = playerHp;
        this.gameState = gameState;
        this.playerState = playerState;
        this.startDateTime = startDateTime;
    }
}
