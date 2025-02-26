using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    // 기존 구매 정보
    public int HPUpgrade;
    public int ArrowATKUpgrade;
    public bool OnBoard;

    public int coins;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/shopData.dat";
        LoadData();
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        ShopData data = new ShopData();
        data.HPUpgrade = HPUpgrade;
        data.ArrowATKUpgrade = ArrowATKUpgrade;
        data.OnBoard = OnBoard;
        data.coins = coins;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            ShopData data = (ShopData)bf.Deserialize(file);
            file.Close();

            HPUpgrade = data.HPUpgrade;
            ArrowATKUpgrade = data.ArrowATKUpgrade;
            OnBoard = data.OnBoard;
            coins = data.coins;
        }
        else
        {
            // 초기값 설정
            HPUpgrade = 0;
            ArrowATKUpgrade = 0;
            OnBoard = false;
            coins = 100;  
        }
    }

    public void AddCoins(int score)
    {
        int coin = Mathf.RoundToInt(score * 0.026f);
        coins += coin;
        SaveData();
    }
}
