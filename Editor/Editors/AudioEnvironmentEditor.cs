using System;
using UnityEditor;
using UnityEditorInternal;

namespace Valax321.AudioSystem.Editor
{
    [CustomEditor(typeof(AudioEnvironment))]
    public class AudioEnvironmentEditor : UnityEditor.Editor
    {
        private SerializedProperty m_layers;

        private ReorderableList m_layersList;
        
        private void OnEnable()
        {
            m_layers = serializedObject.FindProperty("m_layers");
            
            m_layersList = new ReorderableList(serializedObject, m_layers, true, true, true, true);
            m_layersList.drawHeaderCallback += rect => EditorGUI.LabelField(rect, m_layers.displayName);
            m_layersList.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                EditorGUI.PropertyField(rect, m_layers.GetArrayElementAtIndex(index));
            };
            m_layersList.elementHeightCallback +=
                index => EditorGUI.GetPropertyHeight(m_layers.GetArrayElementAtIndex(index)) + 4;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            m_layersList.DoLayoutList();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}