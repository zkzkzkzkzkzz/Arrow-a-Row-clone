using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private ShopItemUI[] itemButtons;

    private void Start()
    {
        UpdateShopUI();
    }

    private void Update()
    {
        coinText.text = ShopManager.Instance.GetPlayerCoins().ToString();
    }

    public void UpdateShopUI()
    {
        foreach (ShopItemUI item in itemButtons)
            item.UpdateButtonState();

        coinText.text = ShopManager.Instance.GetPlayerCoins().ToString();
    }

    public void OnShopPanelClicked()
    {
        shopPanel.SetActive(true);
        UpdateShopUI();
    }

    public void CloseShopPanelClicked()
    {
        shopPanel.SetActive(false);
    }
}
