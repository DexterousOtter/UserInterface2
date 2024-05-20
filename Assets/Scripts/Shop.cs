using System;
using TMPro;
using UnityEngine;

public class Shop : Inventory
{
    public InventoryManagerScript inventoryManagerScript;

    public void SellItem(Item item, int slotIndex)
    {
        if (item.itemType == Item.ItemType.Normal)
        {
            inventoryManagerScript.MoveItemFromShopToBackpack(item, slotIndex);
        }
        else
        {
            Remove(item, slotIndex, 1);
            item.RunItemBoughtAction(item);

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

}