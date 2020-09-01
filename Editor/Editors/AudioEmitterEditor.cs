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
            EditorGUILayout.LabelField("Emitter Properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(m_overrideEmitterProperties);
            
            EditorGUILayout.Separator();
            using (var disabled = new EditorGUI.DisabledGroupScope(!m_overrideEmitterProperties.boolValue))
            {
                EditorGUILayout.PropertyField(m_emitterProperties);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void OnSceneGUI()
        {
            var component = target as AudioEmitter;
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

            var c = Handles.color;
            Handles.color = Color.yellow;
            DrawSphere(t.position, props.minRange);
            DrawSphere(t.position, props.maxRange);
            Handles.color = c;
        }

        private void DrawSphere(Vector3 pos, float radius)
        {
            Handles.DrawWireDisc(pos, Vector3.up, radius);
            Handles.DrawWireDisc(pos, Vector3.forward, radius);
            Handles.DrawWireDisc(pos, Vector3.right, radius);
        }
    }
}
