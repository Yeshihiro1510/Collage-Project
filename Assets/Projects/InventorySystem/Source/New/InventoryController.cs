using Projects.Utils;
using UnityEngine;

namespace Projects.InventorySystem.Source.New
{
    public class InventoryController
    {
        public static string Path => Application.persistentDataPath + "/inventory_data.json";
        
        public InventoryController(InventoryView view)
        {
            view.onDropAll += OnDropAll;
            view.onDropOne += OnDropOne;

            _state = JsonSavingUtil.TryGet<InventoryState>(Path, out var state) ? state : new InventoryState(25);
        }
        
        private InventoryState _state;

        private void OnSwitchAll(int from, int to)
        {
        }

        private void OnSwitchOne(int from, int to)
        {
        }

        private void OnDropAll()
        {
        }

        private void OnDropOne()
        {
        }
    }

    public record DrawPackage(Sprite Icon, string Amount, int Index)
    {
        public static readonly DrawPackage Empty = new(null, string.Empty, 0);
        public Sprite Icon { get; } = Icon;
        public string Amount { get; } = Amount;
        public int Index { get; } = Index;
    }
}