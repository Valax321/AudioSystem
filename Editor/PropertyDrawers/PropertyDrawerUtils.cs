using UnityEditor;
using UnityEngine;

namespace Valax321.AudioSystem.Editor
{
    public static class PropertyDrawerUtils
    {
        public static void DrawProperty(this SerializedProperty prop, ref Rect position)
        {
            var height = EditorGUI.GetPropertyHeight(prop);
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, height),
                prop);
            position.y += height + 1;
        }
    }
}