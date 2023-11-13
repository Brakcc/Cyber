#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitManager))]
public class SpecKapa : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        UnitManager unit = (UnitManager)target;
        if (GUILayout.Button("Restart Turn"))
        {
            unit.ResetLoop();
        }
    }
}
#endif