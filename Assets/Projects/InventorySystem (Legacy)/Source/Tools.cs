using UnityEditor;

namespace Projects.InventorySystem__Legacy_.Source
{
    public static class Tools
    {
        [MenuItem("Inventory/Delete save")]
        public static void DeleteSave()
        {
            ProgressSaver.Delete();
        }
    }
}