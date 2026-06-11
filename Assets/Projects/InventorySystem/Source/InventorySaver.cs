using System.IO;
using UnityEngine;

namespace Projects.InventorySystem.Source
{
    public static class InventorySaver
    {
        private static string _Path => Application.persistentDataPath + "/inventory_data.json";

        public static void Set(InventoryState state)
        {
            var str = JsonUtility.ToJson(state);
            File.WriteAllText(_Path, str);
        }

        public static bool TryGet(out InventoryState result)
        {
            if (File.Exists(_Path))
            {
                var json = File.ReadAllText(_Path);
                result = JsonUtility.FromJson<InventoryState>(json);
                return true;
            }

            result = null;
            return false;
        }

        public static void Delete() => File.Delete(_Path);
    }
}