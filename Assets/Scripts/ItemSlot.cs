using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDragHandler, IDropHandler
{

    public Image itemImage;
    public Image highLightImage;
    public TextMeshProUGUI quantityText;

    public Item item = null;

    public Button button1;
    public Button button2;

    public TextMeshProUGUI priceText;
    public Image coinImage;
    public delegate void ButtonClickHandler(Item item, int slotIndex);

    public ButtonClickHandler button1Action = null;
    public ButtonClickHandler button2Action = null;
    public int slotIndex;
    public int maxQuantity = 10;
    public enum ButtonType
    {
        Button1,
        Button2
    }

    private Tooltip tooltip;

    public delegate void OnDragHandler(Item item, int slotIndex);
    public OnDragHandler onDragHandler;

    public delegate void OnDropHandler(Item item, int slotIndex);
    public OnDropHandler onDropHandler;
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            onDragHandler(item, slotIndex);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {

        onDropHandler(item, slotIndex);

    }

    private void Start()
    {
        tooltip = Tooltip.Instance;
    }

    public int AddItem(Item newItem)
    {
        int amountLeftOver = 0;
        if (item == null)
        {
            item = newItem;
            itemImage.sprite = newItem.itemSprite;
            itemImage.gameObject.SetActive(true);
            quantityText.text = newItem.quantity.ToString();
            priceText.text = "x" + newItem.price.ToString();
            coinImage.gameObject.SetActive(true);

        }
        else
        {
            item.quantity += newItem.quantity;
            quantityText.text = item.quantity.ToString();
        }
        if (item.quantity > maxQuantity)
        {
            amountLeftOver = item.quantity - maxQuantity;
            item.quantity = maxQuantity;
            quantityText.text = item.quantity.ToString();
        }

        return amountLeftOver;
    }

    public void addButtonClickHandler(ButtonType buttonType, ButtonClickHandler action)
    {
        if (buttonType == ButtonType.Button1)
        {
            button1.gameObject.SetActive(true);
            button1Action = action;
            button1.onClick.AddListener(() => action(item, slotIndex));
        }
        else if (buttonType == ButtonType.Button2)
        {
            button2.gameObject.SetActive(true);
            button2Action = action;
            button2.onClick.AddListener(() => action(item, slotIndex));
        }

    }
    public void decrementQuantity(int amountToDecreaseBy)
    {
        item.quantity -= amountToDecreaseBy;
        quantityText.text = item.quantity.ToString();
        if (item.quantity <= 0)
        {
            item = null;
            itemImage.sprite = null;
            quantityText.text = "";
            itemImage.gameObject.SetActive(false);
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            button1Action = null;
            button2Action = null;
            priceText.text = "";
            coinImage.gameObject.SetActive(false);

        }
    }
    public int incrementQuantity(int amountToIncreaseBy)
    {
        if (item.quantity + amountToIncreaseBy <= maxQuantity)
        {
            item.quantity += amountToIncreaseBy;
            quantityText.text = item.quantity.ToString();
            return 0;
        }
        else
        {
            int amountLeftOver = (item.quantity + amountToIncreaseBy) - maxQuantity;
            item.quantity = maxQuantity;
            quantityText.text = item.quantity.ToString();
            return amountLeftOver;
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            tooltip.setTransformFromMousePosition();
            tooltip.toggleActive(true);
            tooltip.setText(item.itemName, item.description);
            highLightImage.gameObject.SetActive(true);

        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.toggleActive(false);
        highLightImage.gameObject.SetActive(false);
    }
}
