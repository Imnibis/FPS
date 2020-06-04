using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LabelAttribute))]
public class LabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect labelPos = new Rect(new Vector2(position.x + 210, position.y + 10),
            position.size);
        LabelAttribute labelAttribute = attribute as LabelAttribute;
        GUIContent labelContent = new GUIContent(labelAttribute.label);

        base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(labelPos, labelContent, property);
        EditorGUI.LabelField(labelPos, labelAttribute.label);
        EditorGUI.EndProperty();
    }
}
