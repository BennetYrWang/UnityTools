using UnityEngine;

namespace Modules
{
    public class Settings
    {
        public const string settingPath = "Assets/Editor/Modules/SaveLoadSystemSettings.asset";

        [SerializeField] private bool hideInInspector = true;
        [SerializeField] private bool hideInHierarchy = false;
        [SerializeField]
    }
}