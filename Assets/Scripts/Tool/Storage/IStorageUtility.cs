using QFramework;

namespace Tool
{
    public interface IStorageUtility : IUtility
    {
        void Save<T>(T data, string fileName, bool config = false);

        T Load<T>(string fileName);
    }
}