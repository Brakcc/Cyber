using UnityEngine;
using UnityEditor;
using Utilities.CustomHideAttribute;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(ShowIfBoolTrue))]
    public class BoolHidePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfBoolTrue hideIfAttribute = (ShowIfBoolTrue)attribute;
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
            ShowIfBoolTrue hideIfAttribute = (ShowIfBoolTrue)attribute;

            if (GetConditionalAttributeResult(hideIfAttribute, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        bool GetConditionalAttributeResult(ShowIfBoolTrue attribute, SerializedProperty property)
        {
            bool enabled = true;

            string[] boolPropertyPathArray = property.propertyPath.Split('.');
            boolPropertyPathArray[^1] = attribute.kapaProperty;
            string boolPropertyPath = string.Join(".", boolPropertyPathArray);
            
            SerializedProperty kapaValue = property.serializedObject.FindProperty(boolPropertyPath);

            if (kapaValue != null)
            {
                enabled = kapaValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Conditional Attribute not found");
            }

            return enabled;
        }
    }
}
