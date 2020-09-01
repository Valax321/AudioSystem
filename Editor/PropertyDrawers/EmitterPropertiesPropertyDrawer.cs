using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    [CustomPropertyDrawer(typeof(EmitterProperties))]
    public class EmitterPropertiesPropertyDrawer : PropertyDrawer
    {
        private SerializedProperty m_spatialize;
        private SerializedProperty m_blendSpace;
        private SerializedProperty m_dopplerLevel;
        private SerializedProperty m_range;
        private SerializedProperty m_rolloffMode;
        private SerializedProperty m_customRolloffCurve;

        private void PopulateProperties(SerializedProperty parent)
        {
            m_spatialize = parent.FindPropertyRelative("m_spatialize");
            m_blendSpace = parent.FindPropertyRelative("m_blendSpace");
            m_dopplerLevel = parent.FindPropertyRelative("m_dopplerLevel");
            m_range = parent.FindPropertyRelative("m_range");
            m_rolloffMode = parent.FindPropertyRelative("m_rolloffMode");
            m_customRolloffCurve = parent.FindPropertyRelative("m_customRolloffCurve");
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);
            
            float height = 0;

            height += EditorGUI.GetPropertyHeight(m_spatialize) + 2;
            height += EditorGUI.GetPropertyHeight(m_blendSpace) + 2;
            height += EditorGUI.GetPropertyHeight(m_dopplerLevel) + 2;
            height += EditorGUI.GetPropertyHeight(m_range) + 2;
            height += EditorGUI.GetPropertyHeight(m_rolloffMode) + 2;
            height += EditorGUI.GetPropertyHeight(m_customRolloffCurve) + 2;

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);
            EditorGUI.BeginProperty(position, label, property);

            m_spatialize.DrawProperty(ref position);
            using (var disabled = new EditorGUI.DisabledScope(!m_spatialize.boolValue))
            {
                m_blendSpace.DrawProperty(ref position);
                m_dopplerLevel.DrawProperty(ref position);
                m_range.DrawProperty(ref position);
                m_rolloffMode.DrawProperty(ref position);
            }
            using (var disabled =
                new EditorGUI.DisabledScope(!m_spatialize.boolValue || m_rolloffMode.enumValueIndex != (int) AudioRolloffMode.Custom))
            {
                m_customRolloffCurve.DrawProperty(ref position);
            }
            
            EditorGUI.EndProperty();
        }
    }
}