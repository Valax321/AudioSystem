using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    public static class NewGameObjectMenus
    {
        [MenuItem("GameObject/Audio/Audio Emitter", false, 10)]
        private static void NewEmitter(MenuCommand command)
        {
            var go = new GameObject("Audio Emitter", typeof(AudioEmitter));
            GameObjectUtility.SetParentAndAlign(go, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, $"Create {go.name}");
            Selection.activeObject = go;
        }
    }
}
