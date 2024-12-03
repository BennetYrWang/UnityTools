namespace MyModule
{
    public class ModuleManager
    {
        #region Singleton

        private static ModuleManager _instance;
        public static ModuleManager Instance
        {
            get
            {
                    
                return _instance;
            }
        }

        #endregion
}
}