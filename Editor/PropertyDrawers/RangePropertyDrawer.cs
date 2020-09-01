using System;
using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    public abstract class RangePropertyDrawer<T> : PropertyDrawer where T: IComparable<T>
    {
        private static readonly GUIContent[] k_labels = new[]
        {
            new GUIContent("Min"),
            new GUIContent("Max"), 
        };
        
        private SerializedProperty m_min;
        private SerializedProperty m_max;

        private void PopulateProperties(SerializedProperty parent)
        {
            m_min = parent.FindPropertyRelative("min");
            m_max = parent.FindPropertyRelative("max");
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);

            return EditorGUI.GetPropertyHeight(m_min);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);

            EditorGUI.BeginProperty(position, label, property);
            
            EditorGUI.MultiPropertyField(position, k_labels, m_min, label);

            EditorGUI.EndProperty();
        }
    }

    [CustomPropertyDrawer(typeof(FloatRange))]
    public class FloatRangePropertyDrawer : RangePropertyDrawer<float>
    {
    }

    [CustomPropertyDrawer(typeof(IntRange))]
    public class IntRangePropertyDrawer : RangePropertyDrawer<int>
    {
    }
}