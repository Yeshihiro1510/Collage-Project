using UnityEditor;

namespace Projects.InventorySystem.Source
{
    public static class Tools
    {
        [MenuItem("Inventory/Delete save")]
        public static void DeleteSave()
        {
            InventorySaver.Delete();
        }
    }
}