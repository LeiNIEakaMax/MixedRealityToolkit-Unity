﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.﻿

using Microsoft.MixedReality.Toolkit.Editor;
using Microsoft.MixedReality.Toolkit.Utilities.Editor;
using UnityEditor;

namespace Microsoft.MixedReality.Toolkit.Diagnostics.Editor
{
    [CustomEditor(typeof(MixedRealityDiagnosticsProfile))]
    public class MixedRealityDiagnosticsSystemProfileInspector : BaseMixedRealityToolkitConfigurationProfileInspector
    {
        private static bool showGeneralSettings = true;
        private SerializedProperty showDiagnostics;

        private static bool showProfilerSettings = true;
        private SerializedProperty showProfiler;
        private SerializedProperty frameRateDuration;

        // todo: coming soon
        // private static bool showDebugPanelSettings = true;
        // private SerializedProperty isDebugPanelVisible;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!MixedRealityInspectorUtility.CheckMixedRealityConfigured(false))
            {
                return;
            }

            showDiagnostics = serializedObject.FindProperty("showDiagnostics");
            showProfiler = serializedObject.FindProperty("showProfiler");
            frameRateDuration = serializedObject.FindProperty("frameRateDuration");
        }

        public override void OnInspectorGUI()
        {
            RenderMixedRealityToolkitLogo();
            if (!MixedRealityInspectorUtility.CheckMixedRealityConfigured())
            {
                return;
            }

            if (DrawBacktrackProfileButton("Back to Configuration Profile", MixedRealityToolkit.Instance.ActiveProfile))
            {
                return;
            }

            CheckProfileLock(target);

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Diagnostic Visualization Options", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("Diagnostic visualizations can help monitor system resources and performance inside an application.", MessageType.Info);

            EditorGUILayout.Space();
            showGeneralSettings = EditorGUILayout.Foldout(showGeneralSettings, "General Settings", true);
            if (showGeneralSettings)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.PropertyField(showDiagnostics);
                    if(!showDiagnostics.boolValue)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.HelpBox("Diagnostic visualizations have been globally disabled.", MessageType.Info);
                        EditorGUILayout.Space();
                    }
                }
            }

            EditorGUILayout.Space();
            showProfilerSettings = EditorGUILayout.Foldout(showProfilerSettings, "Profiler Settings", true);
            if (showProfilerSettings)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUILayout.PropertyField(showProfiler);
                    EditorGUILayout.PropertyField(frameRateDuration);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
