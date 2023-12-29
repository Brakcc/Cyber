#if UNITY_EDITOR
using GameContent.Entity.Unit.UnitWorking.UnitList;
using UnityEngine;
using UnityEditor;
 
namespace Utilities.CustomHideAttribute
{
    [CustomEditor(typeof(DPSDataSO))]
    public class DPSUnitEditor : Editor
    {
        private DPSDataSO unit;

        private void OnEnable()
        {
            unit = target as DPSDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (unit.Sprite == null)
                return;

            var texture = AssetPreview.GetAssetPreview(unit.Sprite);
            
            GUILayout.Label("", GUILayout.Height(192), GUILayout.Width(96));
            
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
    
    [CustomEditor(typeof(HackerDataSO))]
    public class HackerUnitEditor : Editor
    {
        private HackerDataSO unit;

        private void OnEnable()
        {
            unit = target as HackerDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (unit.Sprite == null)
                return;

            var texture = AssetPreview.GetAssetPreview(unit.Sprite);
            
            
            GUILayout.Label("", GUILayout.Height(192), GUILayout.Width(96));
            
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }
}

#endif