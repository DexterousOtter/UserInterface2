using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabHandler : MonoBehaviour
{
    public GameObject storePanel;
    public GameObject backPackPanel;
    public GameObject chestPanel;

    public Button StoreBackpackButton;
    public Button BackPackChestButton;
    public enum ButtonType{
        StoreBackPack,
        BackPackChest
    }
    public void Start()
    {
        StoreBackpackButton.onClick.AddListener(() => SwitchTab(ButtonType.StoreBackPack));
        BackPackChestButton.onClick.AddListener(() => SwitchTab(ButtonType.BackPackChest));

  

    }
    public void SwitchTab(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.StoreBackPack:
                chestPanel.SetActive(false);
                storePanel.SetActive(true);
                break;
            case ButtonType.BackPackChest:
                storePanel.SetActive(false);
                chestPanel.SetActive(true);
                break;
        }
    }


}