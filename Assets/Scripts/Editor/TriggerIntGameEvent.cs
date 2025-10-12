using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(IntGameEvent))]

public class TriggerIntGameEvent: Editor
{
    private int raiseValue = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        IntGameEvent myGameEvent = (IntGameEvent)target;
        raiseValue = EditorGUILayout.IntField("Value to Raise", raiseValue);
        if (GUILayout.Button("Raise()"))
        {
            myGameEvent.Raise(raiseValue);
        }

    }

}
