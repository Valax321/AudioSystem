using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    [CustomEditor(typeof(AudioEmitter))]
    public class AudioEmitterEditor : UnityEditor.Editor
    {
        private SerializedProperty m_event;
        private SerializedProperty m_playOnStart;
        private SerializedProperty m_destroyOnComplete;
        private SerializedProperty m_overrideEmitterProperties;
        private SerializedProperty m_emitterProperties;

        private SerializedProperty m_eventEmitterProperties;
        
        private void OnEnable()
        {
            m_event = serializedObject.FindProperty("m_event");
            m_playOnStart = serializedObject.FindProperty("m_playOnStart");
            m_destroyOnComplete = serializedObject.FindProperty("m_destroyGameObjectOnComplete");
            m_overrideEmitterProperties = serializedObject.FindProperty("m_overrideEmitterProperties");
            m_emitterProperties = serializedObject.FindProperty("m_emitterProperties");

            if (m_event.objectReferenceValue)
            {
                m_eventEmitterProperties = m_event.FindPropertyRelative("m_emitterProperties");
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(m_event);
            
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(m_playOnStart);
            EditorGUILayout.PropertyField(m_destroyOnComplete);
            
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(m_overrideEmitterProperties);
            using (var disabled = new EditorGUI.DisabledGroupScope(!m_overrideEmitterProperties.boolValue))
            {
                EditorGUILayout.PropertyField(m_emitterProperties);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        [DrawGizmo(GizmoType.InSelectionHierarchy, typeof(AudioEmitter))]
        private static void DrawGizmos(AudioEmitter component, GizmoType gizmoType)
        {
            EmitterProperties props = null;
            
            if (component.overrideEmitterProperties)
            {
                props = component.emitterProperties;
            }
            else if (component.audioEvent)
            {
                props = component.audioEvent.emitterProperties;
            }

            if (props is null)
                return;

            if (!props.spatialize)
                return;

            var t = component.transform;

            var c = Gizmos.color;
            Gizmos.color = new Color(215, 173, 8);
            Gizmos.DrawWireSphere(t.position, props.minRange);
            Gizmos.DrawWireSphere(t.position, props.maxRange);
            Gizmos.color = c;
        }
    }
}
