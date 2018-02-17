using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubWave.EnemyCount))]
public class EnemyCountPropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // var indent = EditorGUI.indentLevel;
        // EditorGUI.indentLevel = 0;

        float countControlWidth = 65f;
        float prefabControlWidth = position.width - countControlWidth;

        var countControlRect = new Rect(position.x, position.y, countControlWidth, position.height);
        var prefabControlRect = new Rect(position.x + countControlWidth - 20, position.y, prefabControlWidth, position.height);
        var labelRect = new Rect(position.x + countControlWidth - 40, position.y, 65, position.height);

        EditorGUI.PropertyField(prefabControlRect, property.FindPropertyRelative("m_EnemyPrefab"), GUIContent.none);
        EditorGUI.PropertyField(countControlRect, property.FindPropertyRelative("m_SpawnCount"), GUIContent.none);
        EditorGUI.LabelField(labelRect, "x");

        //EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}