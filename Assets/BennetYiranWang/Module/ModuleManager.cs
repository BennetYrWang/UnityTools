using System.Collections.Generic;
using UnityEngine;

namespace BennetYiranWang.Module
{
    [ExecuteInEditMode]
    public class ModuleManager : MonoBehaviour
    {
        private static ModuleManager _instance;
        private bool _initialized;
        private static readonly object Lock = new object();

        public static ModuleManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance != null) return _instance;
                        _instance = new GameObject(ModuleManagerSettings.LoadOrCreateSettings().GetSettingsData().runTimeGeneratedGameObjectName)
                            .AddComponent<ModuleManager>();
                        _instance.Init(true);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance && _instance != this)
            {
                Destroy(this);
                Debug.LogWarning("Another instance of ModuleManager already exists. Destroying this one.");
                return;
            }
            _instance = this;

            if (!_initialized)
                Init(false);
        }

        private void Init(bool runTimeGenerated)
        {
            var settings = ModuleManagerSettings.LoadOrCreateSettings().GetSettingsData();
#if UNITY_EDITOR
            if (!settings.allowCreateModuleManagerInEditor)
            {
                Debug.LogWarning("ModuleManager is not allowed in the scene based on current settings.");
            }
#endif
            
            hideFlags = (settings.hideInInspector ? HideFlags.HideInInspector : 0) |
                        (settings.hideInHiderarchy ? HideFlags.HideInHierarchy : 0);
            
            if (settings.dontDestroyModuleManagerOnLoad)
                DontDestroyOnLoad(gameObject);
            if (runTimeGenerated)
                gameObject.name = settings.runTimeGeneratedGameObjectName;

            _initialized = true;
        }
    }
}