using TMPro;

public class Backpack : Inventory
{
    public InventoryManagerScript inventoryManagerScript;
    public void SellItem(Item item, int slotIndex)
    {
        inventoryManagerScript.MoveItemFromBackpackToShop(item, slotIndex);
    }
    public void StoreItem(Item item, int slotIndex)
    {
        inventoryManagerScript.MoveItemFromBackpackToChest(item, slotIndex);
    }
    public override void UpdateInventory()
    {
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item != null)
            {
                if (itemSlot.button1Action == null)
                {
                    itemSlot.addButtonClickHandler(ItemSlot.ButtonType.Button1, SellItem);
                    itemSlot.button1.GetComponentInChildren<TextMeshProUGUI>().text = "Sell";
                }
                if (itemSlot.button2Action == null)
                {
                    itemSlot.addButtonClickHandler(ItemSlot.ButtonType.Button2, StoreItem);
                    itemSlot.button2.GetComponentInChildren<TextMeshProUGUI>().text = "Store";
                }
            }
        }

    }
}