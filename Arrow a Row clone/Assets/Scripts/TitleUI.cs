using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;

    public void OnShopButtonClicked()
    {
        if (shopUI != null)
            shopUI.SetActive(true);
    }

    public void OnCloseShopButtonClicked()
    {
        if (shopUI != null)
            shopUI.SetActive(false);
    }
}
