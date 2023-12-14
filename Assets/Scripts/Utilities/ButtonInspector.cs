#if UNITY_EDITOR
using GameContent.Entity.Unit.UnitWorking;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
    [CustomEditor(typeof(UnitManager))]
    public class SpecKapa : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            UnitManager unit = (UnitManager)target;
            if (GUILayout.Button("Restart Turn"))
            {
                UnitManager.ResetLoop();
            }
        }
    }
}
#endif