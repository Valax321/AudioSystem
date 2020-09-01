using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    [CustomPropertyDrawer(typeof(AudioEnvironmentLayer))]
    public class AudioEnvironmentLayerPropertyDrawer : PropertyDrawer
    {
        private SerializedProperty m_type;
        private SerializedProperty m_event;
        private SerializedProperty m_subEnvironment;

        private SerializedProperty m_positionKey;
        private SerializedProperty m_positionOffset;
        private SerializedProperty m_randomRadius;
        private SerializedProperty m_playFrequency;
        
        private void PopulateProperties(SerializedProperty parent)
        {
            m_type = parent.FindPropertyRelative("m_type");
            m_event = parent.FindPropertyRelative("m_event");
            m_subEnvironment = parent.FindPropertyRelative("m_subEnvironment");

            m_positionKey = parent.FindPropertyRelative("m_positionKey");
            m_positionOffset = parent.FindPropertyRelative("m_positionOffset");
            m_randomRadius = parent.FindPropertyRelative("m_randomRadius");
            m_playFrequency = parent.FindPropertyRelative("m_playFrequency");
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);
            
            var type = (AudioEnvironmentType) m_type.enumValueIndex;

            var size = EditorGUI.GetPropertyHeight(m_type) + 2
                       + EditorGUI.GetPropertyHeight(type == AudioEnvironmentType.SubEnvironment
                           ? m_subEnvironment
                           : m_event) + 2;

            if (type != AudioEnvironmentType.SubEnvironment)
            {
                size += EditorGUIUtility.singleLineHeight;
                
                size += EditorGUI.GetPropertyHeight(m_positionKey) + 2
                        + EditorGUI.GetPropertyHeight(m_positionOffset) + 2
                        + EditorGUI.GetPropertyHeight(m_randomRadius) + 2;

                if (type == AudioEnvironmentType.RandomInstanced)
                {
                    size += EditorGUI.GetPropertyHeight(m_playFrequency) + 2;
                }
            }

            return size;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PopulateProperties(property);

            EditorGUI.BeginProperty(position, label, property);
            
            m_type.DrawProperty(ref position);
            var type = (AudioEnvironmentType) m_type.enumValueIndex;
            switch (type)
            {
                case AudioEnvironmentType.SubEnvironment:
                    m_subEnvironment.DrawProperty(ref position);
                    break;
                default:
                    m_event.DrawProperty(ref position);
                    break;
            }

            if (type != AudioEnvironmentType.SubEnvironment)
            {
                position.y += EditorGUIUtility.singleLineHeight;
                m_positionKey.DrawProperty(ref position);
                m_positionOffset.DrawProperty(ref position);
                m_randomRadius.DrawProperty(ref position);

                if (type == AudioEnvironmentType.RandomInstanced)
                {
                    m_playFrequency.DrawProperty(ref position);
                }
            }

            EditorGUI.EndProperty();
        }
    }
}