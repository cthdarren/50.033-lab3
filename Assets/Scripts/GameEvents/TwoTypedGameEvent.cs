using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameEvent<T, U> : ScriptableObject
{
    private readonly List<GameEventListener<T, U>> eventListeners =
        new List<GameEventListener<T, U>>();

    public void Raise(T data, U seconddata)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(data, seconddata);
    }

    public void RegisterListener(GameEventListener<T, U> listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener<T, U> listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}