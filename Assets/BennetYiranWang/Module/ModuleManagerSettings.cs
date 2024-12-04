using System;
using UnityEditor;
using UnityEngine;

namespace BennetYiranWang.Module
{
    public class ModuleManagerSettings : ScriptableObject
    {
        public const string SETTINGS_UI_DIRECTORY = "Module";
        public const string SETTINGS_FILE_DIRECTORY = "Editor/Module";
        public const string SETTINGS_FILE_NAME = "ModuleManagerSettings.asset";

        [SerializeField] private bool hideInInspector = false;
        [SerializeField] private bool hideInHierarchy = false;
        [SerializeField] private bool allowCreateModuleManagerInEditor = false;
        [SerializeField] private string runTimeGeneratedGameObjectName = "BennetYiranWang/Modules";
        [SerializeField] private bool dontDestroyModuleManagerOnLoad = true;

        private static ModuleManagerSettings _cachedInstance;

        static ModuleManagerSettings()
        {
            try
            {
                LoadOrCreateSettings();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize ModuleManagerSettings: {e.Message}");
            }
        }

        public static ModuleManagerSettings LoadOrCreateSettings()
        {
            if (_cachedInstance != null)
                return _cachedInstance;

#if UNITY_EDITOR
            string path = $"{SETTINGS_FILE_DIRECTORY}/{SETTINGS_FILE_NAME}";
            _cachedInstance = AssetDatabase.LoadAssetAtPath<ModuleManagerSettings>(path);

            if (_cachedInstance != null)
                return _cachedInstance;

            _cachedInstance = CreateInstance<ModuleManagerSettings>();

            if (!AssetDatabase.IsValidFolder(SETTINGS_FILE_DIRECTORY))
                EnsureFolderExists(SETTINGS_FILE_DIRECTORY);

            AssetDatabase.CreateAsset(_cachedInstance, path);
            AssetDatabase.SaveAssets();
#else
        Debug.LogWarning("Running outside of the Unity Editor, using default settings.");
        _cachedInstance = CreateInstance<ModuleManagerSettings>(); // Default instance
#endif

            return _cachedInstance;
        }

        private static void EnsureFolderExists(string fullPath)
        {
            string[] parts = fullPath.Split('/');
            string currentPath = "Assets";

            foreach (var part in parts)
            {
                string newPath = $"{currentPath}/{part}";
                if (!AssetDatabase.IsValidFolder(newPath))
                    AssetDatabase.CreateFolder(currentPath, part);
                currentPath = newPath;
            }
        }

        public static void RefreshSettings()
        {
#if UNITY_EDITOR
            _cachedInstance = AssetDatabase.LoadAssetAtPath<ModuleManagerSettings>(
                $"{SETTINGS_FILE_DIRECTORY}/{SETTINGS_FILE_NAME}"
            );
#endif
        }

        public SettingsData GetSettingsData()
        {
            return new SettingsData()
            {
                hideInHiderarchy = hideInHierarchy,
                hideInInspector = hideInInspector,
                allowCreateModuleManagerInEditor = allowCreateModuleManagerInEditor,
                dontDestroyModuleManagerOnLoad = dontDestroyModuleManagerOnLoad,
                runTimeGeneratedGameObjectName = runTimeGeneratedGameObjectName
            };
        }
    }
}
