using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CompletionPercentSO", menuName = "CompletionPercentSO")]
public class GameState : ScriptableObject
{
    public int maxCollectibles = 3;
    public int numCollectibles = 0;
    public bool bossDefeated = false;
    public string currentScene;
    public string currentCameraName;
    public DateTime saveStartTime;

    public void collect()
    {
        numCollectibles++;
        if (numCollectibles > maxCollectibles)
            numCollectibles = 3;
    }
    public float calcPercent()
    {
        return (((float)numCollectibles/maxCollectibles)*50f) + ((bossDefeated ? 1 : 0) * 50f); 
    }

    public GameStateSerialized Serialize()
    {
        return new GameStateSerialized
        {
            maxCollectibles=maxCollectibles,
            numCollectibles=numCollectibles,
            bossDefeated=bossDefeated,
            currentScene=currentScene,
            currentCameraName=currentCameraName,
            saveStartTime=saveStartTime
        };
    }
}

[Serializable]
public struct GameStateSerialized
{
    public int maxCollectibles;
    public int numCollectibles;
    public bool bossDefeated;
    public string currentScene;
    public string currentCameraName;
    public DateTime saveStartTime;
}
