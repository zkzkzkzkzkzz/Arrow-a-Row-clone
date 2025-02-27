using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    private string jsonPath;
    private ShopData shopData;
    private Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        jsonPath = Path.Combine(Application.persistentDataPath, "shopData.json");
        LoadShopData();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void LoadShopData()
    {
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            shopData = JsonConvert.DeserializeObject<ShopData>(json);
        }
        else
        {
            shopData = new ShopData();
            shopData.shop.items = new List<ShopItem>
            {
                new ShopItem
                {
                    id = "HP_Upgrade",
                    name = "체력",
                    effects = new List<int> { 100, 100, 100 },
                    prices = new List<int> { 10, 10, 100 },
                    purchasedLevels = new List<bool> { false, false, false }
                },
                new ShopItem
                {
                    id = "ArrowATK",
                    name = "화살 공격력",
                    effects = new List<int> { 1, 1, 1 },
                    prices = new List<int> { 10, 10, 100 },
                    purchasedLevels = new List<bool> { false, false, false }
                }
            };
            SaveShopData();
        }
    }

    public void SaveShopData()
    {
        HashSet<string> existingIds = new HashSet<string>();
        List<ShopItem> filteredItems = new List<ShopItem>();

        foreach (var item in shopData.shop.items)
        {
            if (!existingIds.Contains(item.id))
            {
                existingIds.Add(item.id);
                filteredItems.Add(item);
            }
        }

        shopData.shop.items = filteredItems;

        string json = JsonConvert.SerializeObject(shopData, Formatting.Indented);
        File.WriteAllText(jsonPath, json);
    }

    /// <summary>
    /// 씬이 로드될 때 호출되는 함수
    /// </summary>
    /// <param name="scene">Scene 이름</param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();

        if (player != null)
            ApplyPurchasedItems();
    }

    public bool PurchaseItem(string itemId, int level)
    {
        ShopItem item = shopData.shop.items.Find(i => i.id == itemId);

        if (item == null || level >= item.prices.Count || shopData.player.coins < item.prices[level])
            return false;

        if (item.purchasedLevels[level])
            return false;

        shopData.player.coins -= item.prices[level];
        item.purchasedLevels[level] = true;
        SaveShopData();

        return true;
    }

    private void ApplyPurchasedItems()
    {
        if (player == null)
            return;

        foreach (var item in shopData.shop.items)
        {
            for (int i = 0; i < item.purchasedLevels.Count; ++i)
                if (item.purchasedLevels[i])
                    ApplyItemEffect(item.id, item.effects[i]);
        }
    }

    private void ApplyItemEffect(string itemId, int value)
    {
        switch (itemId)
        {
            case "HP_Upgrade":
                player.IncreaseStat(StatType.HP, value);
                break;
            case "ArrowATK":
                player.IncreaseStat(StatType.ARROWATK, value);
                break;
            default:
                break;
        }
    }

    public int GetPlayerCoins()
    {
        return shopData.player.coins;
    }

    public void SetPlayerCoins(int coin)
    {
        shopData.player.coins = coin;
    }

    public List<ShopItem> GetShopItems()
    {
        return shopData.shop.items;
    }
}


[System.Serializable]
public class ShopData
{
    public PlayerData player;
    public Shop shop;

    public ShopData()
    {
        player = new PlayerData();
        shop = new Shop();
    }
}

[System.Serializable]
public class PlayerData
{
    public int coins = 100;
}

[System.Serializable]
public class Shop
{
    public List<ShopItem> items;
}

[System.Serializable]
public class ShopItem
{
    public string id;
    public string name;
    public List<int> effects;
    public List<int> prices;
    public List<bool> purchasedLevels;
}