using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Normal,
        Upgrade,
        UpgradeableItem
    }
    public string itemName;
    public string description;
    public int quantity;
    public Sprite itemSprite; // Add this line

    public Item itemUpgrade = null;

    public ItemType itemType = ItemType.Normal;
    public int price = 1;

    public Item Clone()
    {
        var clone = CreateInstance<Item>();
        clone.itemName = this.itemName;
        clone.description = this.description;
        clone.quantity = this.quantity;
        clone.itemSprite = this.itemSprite;
        clone.price = this.price;
        clone.itemType = this.itemType;
        clone.itemUpgrade = this.itemUpgrade;

        return clone;
    }

    public void RunItemBoughtAction(Item item) {
        if(itemType == ItemType.UpgradeableItem) {
            InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
            inventoryManagerScript.shop.Add(item.itemUpgrade);
        
        } else if(itemType == ItemType.Upgrade) {
            if(item.itemName == "Chest upgrade") {
                InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
                inventoryManagerScript.chest.IncreaseSize(int.MaxValue);

            } else if(item.itemName == "Backpack upgrade") {
                InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
                inventoryManagerScript.backpack.IncreaseSize(20);

            }
        }
    }
}