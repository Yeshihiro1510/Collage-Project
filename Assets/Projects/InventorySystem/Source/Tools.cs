using Projects.InventorySystem.Source.New;
using Projects.Utils;
using UnityEditor;

namespace Projects.InventorySystem.Source
{
    public static class Tools
    {
        [MenuItem("Inventory/Delete save")]
        public static void DeleteSave() => JsonSavingUtil.Delete(InventoryController.Path);
    }
}