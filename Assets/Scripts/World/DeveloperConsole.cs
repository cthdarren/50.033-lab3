using UnityEngine;

public class DeveloperConsole : MonoBehaviour
{
    public DevConsole devConsole;
    public BoolVariable isWaitingHitStop;
    // Update is called once per frame
    void Update()
    {
        if (!devConsole.cheatsOn) return;
        if (devConsole.overrideTime)
        {
            if (isWaitingHitStop.Value) Time.timeScale = devConsole.timeScale;
        }
        Application.targetFrameRate = devConsole.targetFrameRate;
    }
}
