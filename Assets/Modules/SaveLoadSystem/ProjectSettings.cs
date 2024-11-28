using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS0414

namespace Modules.SaveLoadSystem
{
    class Settings : ScriptableObject
    {
        public const string settingPath = "Assets/Editor/Modules/SaveLoadSystemSettings.asset";
        public const string defaultBinaryFileSuffix = ".dat";

        [SerializeField] private bool useDefaultSaveFilePath = true;
        [SerializeField] private string saveFilePath;
        [SerializeField] private SaveLoadFormat saveLoadFormat;
        [SerializeField] private bool customizeSuffix = false;
        [SerializeField] private string customizedSuffix = defaultBinaryFileSuffix;
        
        public string GetSaveFilePath() => saveFilePath;

        internal static Settings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<Settings>(settingPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<Settings>();
                settings.saveFilePath = "";
                
                string directoryPath = Path.GetDirectoryName(settingPath);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                
                AssetDatabase.CreateAsset(settings, settingPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
    
    static class ProjectSettingsIMGUIRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting
            var provider = new SettingsProvider("Modules/SaveLoadSystem", SettingsScope.Project)
            {
                label = "SaveLoadSystem",
                guiHandler = (searchContext) =>
                {
                    // Visual adjustments
                    float prevLabelWidth = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 200;
                    
                    // Get settings
                    var settings = Settings.GetSerializedSettings();

                    // File path
                    var useDefaultPath = settings.FindProperty("useDefaultSaveFilePath");
                    EditorGUILayout.PropertyField(useDefaultPath, new GUIContent("Use Default Save File Path"));
                    if (!useDefaultPath.boolValue)
                    {
                        EditorGUILayout.PropertyField(settings.FindProperty("saveFilePath"),
                            new GUIContent("Save File Path"));
                    }
                    
                    // Suffix
                    var format = settings.FindProperty("saveLoadFormat");
                    EditorGUILayout.PropertyField(format, new GUIContent("Save File Format"));
                    if (format.enumValueIndex == (int)SaveLoadFormat.Binary)
                    {
                        var customizeSuffix = settings.FindProperty("customizeSuffix");
                        EditorGUILayout.PropertyField(customizeSuffix, new GUIContent("Customize Suffix"));
                        if (customizeSuffix.boolValue)
                        {
                            var suffix = settings.FindProperty("customizedSuffix");
                            EditorGUILayout.PropertyField(suffix, new GUIContent("Customized Suffix"));
                            string suffixInput = suffix.stringValue;
                            if (string.IsNullOrWhiteSpace(suffixInput) || !IsValidSuffix(suffixInput))
                                suffix.stringValue = Settings.defaultBinaryFileSuffix;
                            // Add '.' if it does not exist
                            if (suffix.stringValue[0] != '.')
                                suffix.stringValue = '.' + suffix.stringValue;
                        }
                    }


                    
                    
                    settings.ApplyModifiedProperties();
                    EditorGUIUtility.labelWidth = prevLabelWidth;
                },
                keywords = new HashSet<string>(new[] {"saveFilePath"})
            };
            return provider;
        }
        
        public static bool IsValidSuffix(string suffix)
        {
            if (string.IsNullOrWhiteSpace(suffix)) return false;
            return Regex.IsMatch(suffix, @"^\.[a-zA-Z0-9]+$");
        }
    }
}

#pragma warning restore CS0414