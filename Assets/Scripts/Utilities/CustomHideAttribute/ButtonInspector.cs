#if UNITY_EDITOR
using GameContent.Entity.Unit.UnitWorking;
using UnityEditor;
using UnityEngine;

namespace Utilities.CustomHideAttribute
{
    [CustomEditor(typeof(UnitManager))]
    public class SpecKapa : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var  unit = (UnitManager)target;
            if (GUILayout.Button("Restart Turn"))
            {
                UnitManager.ResetLoop();
            }
        }
    }
}
#endif