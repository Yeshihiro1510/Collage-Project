using System.IO;
using UnityEngine;

namespace Projects.Utils
{
    public static class JsonSavingUtil
    {
        public static void Set<T>(T state, string path)
        {
            var str = JsonUtility.ToJson(state);
            File.WriteAllText(path, str);
        }

        public static bool TryGet<T>(string path, out T result)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
                return true;
            }

            result = default;
            return false;
        }

        public static void Delete(string path) => File.Delete(path);
    }
}