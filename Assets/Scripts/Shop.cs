using System;
using TMPro;
using UnityEngine;

public class Shop : Inventory
{
    public InventoryManagerScript inventoryManagerScript;

    public void SellItem(Item item, int slotIndex)
    {
        if (item.itemType == Item.ItemType.Normal || item.itemType == Item.ItemType.UpgradeableItem || item.itemType == Item.ItemType.Unique)
        {
           bool itemTransfered = inventoryManagerScript.MoveItemFromShopToBackpack(item, slotIndex);
            if(item.itemType == Item.ItemType.UpgradeableItem && itemTransfered)
            {
                item.RunItemBoughtAction(item);
            }
        }
        else if (item.itemType == Item.ItemType.Upgrade)
        {
            if(item.RunItemBoughtAction(item))
            {
                Remove(item, slotIndex, 1);
            }

        }
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
                    itemSlot.button1.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
                }
            }
        }

    }
    private void Start() {
        isOrganizable = false;
    }
}