using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyModule.SaveLoadModule
{
    public class SaveLoadSettings : ScriptableObject
    {
        public const string filePath = "Assets/Editor/MyModule/SaveLoadModule";

        private static SaveLoadSettings instance;
        internal static SaveLoadSettings GetSettings()
        {
            if (instance != null)
                return instance;
            
            var settings = AssetDatabase.LoadAssetAtPath<MyModule.SaveLoadModule.SaveLoadSettings>(filePath);
            if (settings != null)
                return settings;

            string directoryPath = Path.GetDirectoryName(filePath);
            settings = ScriptableObject.CreateInstance<SaveLoadSettings>();

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            AssetDatabase.CreateAsset(settings, filePath);
            AssetDatabase.SaveAssets();

            return settings;
        }
    }
}