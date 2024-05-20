using TMPro;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    public Shop shop;
    public Backpack backpack;
    public Chest chest;
    public Item[] allItems;

    public int playerMoney = 100;

    public TextMeshProUGUI moneyText;
    public void Start()
    {
        shop.Instantiate();
        backpack.Instantiate();
        chest.Instantiate();
        foreach (Item item in allItems)
        {
            Item itemClone = item.Clone();
            shop.Add(itemClone);
        }
        InvokeRepeating("AddNewItemsToShop", 10f, 10f);
    }
    public bool MoveItemFromShopToBackpack(Item item, int shopSlotIndex)
    {
        Item itemToMove = item.Clone();
        itemToMove.quantity = 1;
        if (playerMoney - itemToMove.price < 0)
        {
            return false;
        }
        int amountLeftOver = backpack.Add(itemToMove);
        if (amountLeftOver == 0)
        {
            shop.Remove(item, shopSlotIndex, 1);
            updateMoney(itemToMove.price);
            return true;
        }

        return false;
    }

    public bool MoveItemFromBackpackToShop(Item item, int backpackSlotIndex)
    {
        Item itemToMove = item.Clone();
        itemToMove.quantity = 1;
        int amountLeftOver = shop.Add(itemToMove);
        if (amountLeftOver == 0)
        {
            backpack.Remove(item, backpackSlotIndex, 1);
            updateMoney(-itemToMove.price);
            return true;
        }

        return false;
    }

    public bool MoveItemFromBackpackToChest(Item item, int backpackSlotIndex)
    {
        Item itemToMove = item.Clone();
        itemToMove.quantity = 1;
        int amountLeftOver = chest.Add(itemToMove);
        if (amountLeftOver == 0)
        {
            backpack.Remove(item, backpackSlotIndex, 1);
            return true;
        }

        return false;
    }
    public bool MoveItemFromChestToBackpack(Item item, int chestSlotIndex)
    {
        Item itemToMove = item.Clone();
        itemToMove.quantity = 1;
        int amountLeftOver = backpack.Add(itemToMove);
        if (amountLeftOver == 0)
        {
            chest.Remove(item, chestSlotIndex, 1);
            return true;
        }

        return false;
    }
    private void updateMoney(int amount)
    {
        playerMoney -= amount;
        moneyText.text = "x " + playerMoney.ToString();
    }
    private void AddNewItemsToShop()
    {
        foreach (Item item in allItems)
        {
            if (item.itemType == Item.ItemType.Normal)
            {
                Item itemClone = item.Clone();
                itemClone.quantity = 1;
                int itemsLeftOver = shop.Add(itemClone);
                if(itemsLeftOver > 0)
                {
                   return; //no more items can be added 
                }
            }

        }
    }
}