using TMPro;

public class Chest: Inventory {

    public InventoryManagerScript inventoryManagerScript;

    public void MoveToBackpack(Item item, int slotIndex)
    {
        inventoryManagerScript.MoveItemFromChestToBackpack(item, slotIndex);
    }
    public override void UpdateInventory()
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item != null)
            {
                if (itemSlot.button1Action == null)
                {
                    itemSlot.addButtonClickHandler(ItemSlot.ButtonType.Button1, MoveToBackpack);
                    itemSlot.button1.GetComponentInChildren<TextMeshProUGUI>().text = "Carry";
                }
            }
        }
     
    }
}