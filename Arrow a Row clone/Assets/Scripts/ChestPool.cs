using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPool : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private int poolSize = 3;

    [SerializeField] private GameObject bossChestPrefab;
    [SerializeField] private int poolSize2 = 3;

    private Queue<GameObject> chestPool = new Queue<GameObject>();
    private Queue<GameObject> bossChestPool = new Queue<GameObject>();

    [SerializeField] private Transform poolContainer;

    private void Start()
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = Instantiate(chestPrefab);
            obj.SetActive(false);
            chestPool.Enqueue(obj);
            obj.transform.SetParent(poolContainer);
        }

        for (int i = 0; i < poolSize2; ++i)
        {
            GameObject obj = Instantiate(bossChestPrefab);
            obj.SetActive(false);
            bossChestPool.Enqueue(obj);
            obj.transform.SetParent(poolContainer);
        }
    }

    public GameObject GetChest()
    {
        if (chestPool.Count > 0)
        {
            GameObject @object = chestPool.Dequeue();
            @object.SetActive(true);
            return @object;
        }
        else
        {
            GameObject @object = Instantiate(chestPrefab);
            return @object;
        }
    }

    public void ReturnChest(GameObject chest)
    {
        chest.transform.SetParent(poolContainer);
        chest.SetActive(false);
        chestPool.Enqueue(chest);
    }

    public GameObject GetBossChest()
    {
        if (bossChestPool.Count > 0)
        {
            GameObject @object = bossChestPool.Dequeue();
            @object.SetActive(true);
            return @object;
        }
        else
        {
            GameObject @object = Instantiate(bossChestPrefab);
            return @object;
        }
    }

    public void ReturnBossChest(GameObject chest)
    {
        chest.transform.SetParent(poolContainer);
        chest.SetActive(false);
        bossChestPool.Enqueue(chest);
    }
}
