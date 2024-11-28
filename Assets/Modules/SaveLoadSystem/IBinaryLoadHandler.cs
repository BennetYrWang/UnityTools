namespace Modules.SaveLoadSystem
{
    public interface IBinaryLoadHandler
    {
        bool IsValidData(byte[] data);
        void LoadData(byte[] data);
    }
}