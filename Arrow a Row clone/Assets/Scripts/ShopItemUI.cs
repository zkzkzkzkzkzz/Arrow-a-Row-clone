using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] string itemId;
    [SerializeField] int level;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button purchaseButton;

    private void Start()
    {
        UpdateButtonState();
    }

    public void OnPurchaseButtonClicked()
    {
        bool isPurchase = ShopManager.Instance.PurchaseItem(itemId, level);
        if (isPurchase)
        {
            UpdateButtonState();
            FindObjectOfType<ShopUI>().UpdateShopUI();
        }
    }

    public void UpdateButtonState()
    {
        // 현재 itemId를 가진 아이템 찾기
        //var item = ShopManager.Instance.GetShopItems().Find(i => i.id == itemId);

        var items = ShopManager.Instance.GetShopItems();
        var item = items.Find(i => i.id == itemId);

        if (item.purchasedLevels[level])
        {
            priceText.text = "구매함";
            purchaseButton.interactable = false;
        }
        else
        {
            priceText.text = item.prices[level].ToString();
            purchaseButton.interactable = true;
        }
    }
}
