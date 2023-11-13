using UnityEngine;
using UnityEditor;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(ShowIfTrue))]
    public class HidePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfTrue hideIfAttribute = (ShowIfTrue)attribute;
            if (GetConditionalAttributeResult(hideIfAttribute, property))
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfTrue hideIfAttribute = (ShowIfTrue)attribute;

            if (GetConditionalAttributeResult(hideIfAttribute, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        bool GetConditionalAttributeResult(ShowIfTrue attribute, SerializedProperty property)
        {
            bool enabled = true;

            string[] boolPropertyPathArray = property.propertyPath.Split('.');
            boolPropertyPathArray[^1] = attribute.kapaProperty;
            string boolPropertyPath = string.Join(".", boolPropertyPathArray);

            int boolP = attribute.kapaF;
            //CameraEffectType boolC = attribute.camT;

            SerializedProperty kapaValue = property.serializedObject.FindProperty(boolPropertyPath);

            if (kapaValue != null)
            {
                enabled = kapaValue.enumValueIndex == boolP;
            }
            else
            {
                Debug.LogWarning("Conditional Attribute not found");
            }

            return enabled;
        }
    }
}
