namespace Modules
{
    public class ModulesManager
    {
        #region Singleton

        private static ModulesManager _instance;
        public static ModulesManager Instance
        {
            get
            {
                if (!_instance)
                return _instance;
            }
        }

        #endregion
}
}