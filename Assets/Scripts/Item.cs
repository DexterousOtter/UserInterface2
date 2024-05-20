using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Normal,
        Unique,
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

    public bool RunItemBoughtAction(Item item) {
        if(itemType == ItemType.UpgradeableItem) {
            InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
            inventoryManagerScript.shop.Add(item.itemUpgrade);
            return true;
        
        } else if(itemType == ItemType.Upgrade) {
            if(item.itemName == "Chest upgrade") {
                InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
                if(inventoryManagerScript.playerMoney - item.price < 0) {
                    return false;
                }
                inventoryManagerScript.chest.IncreaseSize(int.MaxValue);
                inventoryManagerScript.playerMoney -= item.price;
                inventoryManagerScript.moneyText.text = "x " + inventoryManagerScript.playerMoney;


            } else if(item.itemName == "Backpack upgrade") {
                InventoryManagerScript inventoryManagerScript = GameObject.Find("Content").GetComponent<InventoryManagerScript>();
                if(inventoryManagerScript.playerMoney - item.price < 0) {
                    return false;
                }
                inventoryManagerScript.backpack.IncreaseSize(8);
                inventoryManagerScript.playerMoney -= item.price;
                inventoryManagerScript.moneyText.text = "x " + inventoryManagerScript.playerMoney;

            }
            return true;
        }
        return true;
    }
}