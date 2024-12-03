using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyModule
{
    public class Settings : ScriptableObject
    {
        public const string SettingDirectory = "Assets/Editor/MyModule/";
        public const string ProjectSettingDirectory = "MyModule";
        public const string FileName = "ModuleSettings.asset";

        [SerializeField] private bool hideInInspector = true;
        [SerializeField] private bool hideInHierarchy = false;


        internal static Settings getOrCreateSettings()
        {
            string SettingPath = SettingDirectory + FileName;
            var settings = AssetDatabase.LoadAssetAtPath<Settings>(SettingPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<Settings>();

                if (!Directory.Exists(SettingDirectory))
                    Directory.CreateDirectory(SettingDirectory);
                
                AssetDatabase.CreateAsset(settings,SettingPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(getOrCreateSettings());
        }
    }

    static class ProjectSettingsIMGUIRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new SettingsProvider(Settings.ProjectSettingDirectory, SettingsScope.Project)
            {
                
            };
            return provider;
        }
    }
}