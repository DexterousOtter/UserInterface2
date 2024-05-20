using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Inventory : MonoBehaviour
{
    public List<ItemSlot> itemSlots;
    public ItemSlot itemSlotPrefab;
    public Transform itemSlotParent;
    public int space = 20;
    private int maxEmptySlotsToRender = 20;

    private int itemBeingDraggedSlotIndex;
    private Item itemBeingDragged;

    public int Add(Item item)
    {

        int itemsLeftOver = item.quantity;
        Item itemToAdd = item;
        //Add to pre-existing stacks
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (!itemSlot.item) continue;
            if (itemSlot.item.itemName == item.itemName)
            {
                itemsLeftOver = itemSlot.AddItem(item);
            }
            if (itemsLeftOver == 0)
            {
                break;
            }
            else
            {
                itemToAdd = item.Clone();
                itemToAdd.quantity = itemsLeftOver;
            }
        }
        //Add to empty slots
        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item == null)
            {
                itemsLeftOver = itemSlot.AddItem(itemToAdd);
            }
            if (itemsLeftOver == 0)
            {
                break;
            }
            else
            {
                itemToAdd = item.Clone();
                itemToAdd.quantity = itemsLeftOver;
            }
        }
        //Add to new slots
        while (itemsLeftOver > 0 && itemSlots.Count < space)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlot.slotIndex = itemSlots.Count;
            itemsLeftOver = itemSlot.AddItem(itemToAdd);
            itemSlots.Add(itemSlot);
            itemToAdd = item.Clone();
            itemToAdd.quantity = itemsLeftOver;

        }
        UpdateInventory();

        return itemsLeftOver;

    }
    public void Remove(Item item, int slotIndex, int quantityToRemove)
    {
        ItemSlot itemSlot = itemSlots.Find(x => x.slotIndex == slotIndex);
        if (itemSlot != null)
        {
            itemSlot.decrementQuantity(quantityToRemove);
        }

    }
    public abstract void UpdateInventory();

    public void Instantiate()
    {
        itemSlots = new List<ItemSlot>();
        while (itemSlots.Count < space && itemSlots.Count < maxEmptySlotsToRender)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlot.slotIndex = itemSlots.Count;
            itemSlot.item = null;
            itemSlot.quantityText.text = "";
            itemSlot.itemImage.sprite = null;
            itemSlot.itemImage.gameObject.SetActive(false);
            itemSlot.priceText.text = "";
            itemSlot.coinImage.gameObject.SetActive(false);
            itemSlot.onDragHandler = (Item item, int slotIndex) =>
            {
                itemBeingDragged = item;
                itemBeingDraggedSlotIndex = slotIndex;
            };
            itemSlot.onDropHandler = (Item item, int slotIndex) => OnDropHandler(item, slotIndex);
            itemSlots.Add(itemSlot);

        }
    }
    public void IncreaseSize(int amount)
    {
        space = amount;
        while (itemSlots.Count < space && itemSlots.Count < maxEmptySlotsToRender)
        {
            ItemSlot itemSlot = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlot.slotIndex = itemSlots.Count;
            itemSlot.item = null;
            itemSlot.quantityText.text = "";
            itemSlot.itemImage.sprite = null;
            itemSlot.itemImage.gameObject.SetActive(false);
            itemSlot.priceText.text = "";
            itemSlot.coinImage.gameObject.SetActive(false);
             itemSlot.onDragHandler = (Item item, int slotIndex) =>
            {
                itemBeingDragged = item;
                itemBeingDraggedSlotIndex = slotIndex;
            };
            itemSlot.onDropHandler = (Item item, int slotIndex) => OnDropHandler(item, slotIndex);
            itemSlots.Add(itemSlot);

        }
        UpdateInventory();
    }
    public void OnDropHandler(Item item, int slotIndex)
    {

        ItemSlot draggedItemSlot = itemSlots.Find((x) => x.slotIndex == itemBeingDraggedSlotIndex);
        ItemSlot targetItemSlot = itemSlots.Find((x) => x.slotIndex == slotIndex);
        if (draggedItemSlot != null && targetItemSlot != null)
        {
            if (draggedItemSlot.item != null && targetItemSlot.item != null)
            {
                if (draggedItemSlot.item.itemName == targetItemSlot.item.itemName)
                {
                    int amountLeftOver = targetItemSlot.AddItem(draggedItemSlot.item);
                    draggedItemSlot.item.quantity = amountLeftOver;
                    draggedItemSlot.quantityText.text = draggedItemSlot.item.quantity.ToString();
                    if (amountLeftOver == 0)
                    {
                        draggedItemSlot.item = null;
                        draggedItemSlot.quantityText.text = "";
                        draggedItemSlot.itemImage.sprite = null;
                        draggedItemSlot.itemImage.gameObject.SetActive(false);
                        draggedItemSlot.priceText.text = "";
                        draggedItemSlot.coinImage.gameObject.SetActive(false);
                        draggedItemSlot.button1.gameObject.SetActive(false);
                        draggedItemSlot.button2.gameObject.SetActive(false);
                        draggedItemSlot.button1Action = null;
                        draggedItemSlot.button2Action = null;
                    }
                }
                else
                {
                    Item tempItem = draggedItemSlot.item;
                    draggedItemSlot.item = targetItemSlot.item;
                    targetItemSlot.item = tempItem;
                    draggedItemSlot.quantityText.text = draggedItemSlot.item.quantity.ToString();
                    targetItemSlot.quantityText.text = targetItemSlot.item.quantity.ToString();
                    draggedItemSlot.itemImage.sprite = draggedItemSlot.item.itemSprite;
                    targetItemSlot.itemImage.sprite = targetItemSlot.item.itemSprite;
                    draggedItemSlot.priceText.text = "x" + draggedItemSlot.item.price.ToString();
                    targetItemSlot.priceText.text = "x" + targetItemSlot.item.price.ToString();
                }
            }
            else if (draggedItemSlot.item != null && targetItemSlot.item == null)
            {
                targetItemSlot.item = draggedItemSlot.item;
                targetItemSlot.quantityText.text = targetItemSlot.item.quantity.ToString();
                targetItemSlot.itemImage.sprite = targetItemSlot.item.itemSprite;
                targetItemSlot.itemImage.gameObject.SetActive(true);
                targetItemSlot.priceText.text = "x" + targetItemSlot.item.price.ToString();
                targetItemSlot.coinImage.gameObject.SetActive(true);


                draggedItemSlot.item = null;
                draggedItemSlot.quantityText.text = "";
                draggedItemSlot.priceText.text = "";
                draggedItemSlot.itemImage.sprite = null;
                draggedItemSlot.itemImage.gameObject.SetActive(false);
                draggedItemSlot.coinImage.gameObject.SetActive(false);
                draggedItemSlot.button1.gameObject.SetActive(false);
                draggedItemSlot.button2.gameObject.SetActive(false);
                draggedItemSlot.button1Action = null;
                draggedItemSlot.button2Action = null;

            }
            UpdateInventory();
        }


    }
}