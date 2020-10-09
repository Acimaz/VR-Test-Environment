using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(TestSceneSetup))]
public class TestSceneSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();

        TestSceneSetup setup = (TestSceneSetup) target;
        if (GUILayout.Button("Setup Hands"))
        {
            setup.SetupHands();
            EditorUtility.SetDirty(setup);
            EditorSceneManager.MarkSceneDirty(setup.gameObject.scene);
        }
        if (GUILayout.Button("Remove Hands & Reset Handsmanager"))
        {
            setup.ResetHands();
            EditorUtility.SetDirty(setup);
            EditorSceneManager.MarkSceneDirty(setup.gameObject.scene);
        }
        if (GUILayout.Button("Toggle Original Hands"))
        {
            setup.ToggleOriginalHands();
            EditorUtility.SetDirty(setup);
            EditorSceneManager.MarkSceneDirty(setup.gameObject.scene);
        }
    }
}
