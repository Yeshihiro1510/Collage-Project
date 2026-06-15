namespace Projects.InventorySystem__Legacy_.Source
{
    public class InventoryController
    {
        public InventoryController(InventoryWindowView windowView, InventoryModel model)
        {
            model.onSlotsChanged += windowView.RedrawSlots;
            model.onItemsChanged += windowView.RedrawItems;
            model.onChanged += ProgressSaver.Set;
            windowView.onSwitch += model.Switch;
            windowView.onExtract += model.Extract;
            windowView.onDropAll += model.DropAll;
            windowView.onDropOne += model.DropOne;

            windowView.Initialize();
            if (ProgressSaver.TryGet(out var state) && state.slots.Length > 0) model.Initialize(state);
            else model.Initialize(25);
        }
    }
}