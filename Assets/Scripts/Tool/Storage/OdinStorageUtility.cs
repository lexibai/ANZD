using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LogTool;
using Sirenix.Serialization;
using UnityEngine;

namespace Tool
{
    public class OdinStorageUtility : IStorageUtility
    {
        public void Save<T>(T data, string fileName, bool config = false)
        {
            string path = Path.Combine(
                config ? Application.streamingAssetsPath : Application.persistentDataPath,
                $"{fileName}.bytes"
            );

            // 使用Odin序列化
            byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
            File.WriteAllBytes(path, bytes);
        }



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
            string path = GetFullPath(fileName, config);
            byte[] bytes = File.ReadAllBytes(path);
            
            // 使用Odin反序列化
            T data = SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
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
                $"{fileName}.bytes"
            );
        }

    }
}