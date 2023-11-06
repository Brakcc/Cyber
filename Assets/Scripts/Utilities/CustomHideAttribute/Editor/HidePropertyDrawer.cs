using UnityEngine;
using UnityEditor;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(HideIfFalse))]
    public class HidePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HideIfFalse hideIfAttribute = (HideIfFalse)attribute;
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
            HideIfFalse hideIfAttribute = (HideIfFalse)attribute;

            if (GetConditionalAttributeResult(hideIfAttribute, property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        bool GetConditionalAttributeResult(HideIfFalse attribute, SerializedProperty property)
        {
            bool enabled = true;

            string[] boolPropertyPathArray = property.propertyPath.Split('.');
            boolPropertyPathArray[^1] = attribute.kapaProperty;
            string boolPropertyPath = string.Join(".", boolPropertyPathArray);

            KapaFunctionType boolP = attribute.kapaF;

            SerializedProperty kapaValue = property.serializedObject.FindProperty(boolPropertyPath);

            if (kapaValue != null)
            {
                enabled = kapaValue.enumValueIndex == (int)boolP;
            }
            else
            {
                Debug.LogWarning("Conditional Attribute not found");
            }

            return enabled;
        }
    }
}
