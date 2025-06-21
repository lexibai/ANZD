using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LogTool;
using QFramework;
using UnityEngine;

namespace Tool
{
    public class BinaryStorageUtility : IUtility
    {
        public void Save<T>(T data, string fileName, bool config = false)
        {
            string path = Path.Combine(
                config ? Application.streamingAssetsPath : Application.persistentDataPath,
                $"{fileName}.bin"
            );
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, data);
            stream.Close();
        }


        // 从文件加载数据
        public T Load<T>(string fileName)
        {
            if (FileExists(fileName, true))
            {
                return Data<T>(fileName, true);
            }


            if (FileExists(fileName, false))
            {
                return Data<T>(fileName, false);
            }
            return default(T);
        }

        private T Data<T>(string fileName, bool config)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream stream = new FileStream(GetFullPath(fileName, config), FileMode.Open);
            T data = (T)formatter.Deserialize(stream);
            stream.Close();
            XLog.Instance.info($"成功加载文件 {fileName}");
            return data;
        }


        // 检查文件是否存在
        private bool FileExists(string fileName, bool config)
        {
            return File.Exists(GetFullPath(fileName, config));
        }


        // 获取完整路径（在Unity的持久化数据路径下）
        private string GetFullPath(string fileName, bool config)
        {
            return Path.Combine(
                config ? Application.streamingAssetsPath : Application.persistentDataPath,
                $"{fileName}.bin"
            );
        }
    }
}