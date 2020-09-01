using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;
using Valax321.AudioSystem;

namespace Valax321.AudioSystem.Editor
{
    [CustomEditor(typeof(AudioEvent))]
    public class AudioEventEditor : UnityEditor.Editor
    {
        private static class Strings
        {
            public const string PlaybackFoldoutName = "Playback Properties";
            public const string EmitterFoldoutName = "Emitter Properties";
            
            public const string PlaybackFoldoutKey = "Valax321:AudioSystem:AudioEvent:PlaybackUnfolded";
            public const string EmitterFoldoutKey = "Valax321:AudioSystem:AudioEvent:EmitterUnfolded";
        }
        
        private SerializedProperty m_clipPlayMode;
        private SerializedProperty m_volume;
        private SerializedProperty m_clips;
        private SerializedProperty m_loop;
        private SerializedProperty m_mixerGroup;
        
        private ReorderableList m_clipsList;

        private SerializedProperty m_emitter;

        private void OnEnable()
        {
            GetPlaybackProperties();
            GetEmitterProperties();
        }

        private void GetPlaybackProperties()
        {
            m_clipPlayMode = serializedObject.FindProperty("m_clipPlayMode");
            m_volume = serializedObject.FindProperty("m_volume");
            m_clips = serializedObject.FindProperty("m_clips");
            m_loop = serializedObject.FindProperty("m_loop");
            m_mixerGroup = serializedObject.FindProperty("m_mixerGroup");

            m_clipsList = new ReorderableList(serializedObject, m_clips, true, true, true, true);
            m_clipsList.drawHeaderCallback += rect => { EditorGUI.LabelField(rect, m_clips.displayName); };
            m_clipsList.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.y += 2;
                rect.height -= 2;
                EditorGUI.PropertyField(rect, m_clips.GetArrayElementAtIndex(index), GUIContent.none);
            };
            m_clipsList.elementHeightCallback +=
                index => EditorGUI.GetPropertyHeight(m_clips.GetArrayElementAtIndex(index)) + 4;
        }

        private void GetEmitterProperties()
        {
            m_emitter = serializedObject.FindProperty("m_emitterProperties");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            DrawPlaybackProperties();
            DrawEmitterProperties();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawPlaybackProperties()
        {
            var unfold = EditorPrefs.GetBool(Strings.PlaybackFoldoutKey, true);
            var draw = EditorGUILayout.BeginFoldoutHeaderGroup(unfold, new GUIContent(Strings.PlaybackFoldoutName));
            if (draw)
            {
                EditorGUILayout.PropertyField(m_mixerGroup);
                EditorGUILayout.PropertyField(m_clipPlayMode);
                EditorGUILayout.PropertyField(m_volume);
                EditorGUILayout.PropertyField(m_loop);

                EditorGUILayout.Separator();
                m_clipsList.DoLayoutList();
            }
            
            if (draw != unfold)
            {
                EditorPrefs.SetBool(Strings.PlaybackFoldoutKey, draw);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawEmitterProperties()
        {
            var unfold = EditorPrefs.GetBool(Strings.EmitterFoldoutKey, true);
            var draw = EditorGUILayout.BeginFoldoutHeaderGroup(unfold, new GUIContent(Strings.EmitterFoldoutName));
            if (draw)
            {
                EditorGUILayout.PropertyField(m_emitter);
            }
            
            if (draw != unfold)
            {
                EditorPrefs.SetBool(Strings.EmitterFoldoutKey, draw);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
