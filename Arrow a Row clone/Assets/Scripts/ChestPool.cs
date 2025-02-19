using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPool : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private int poolSize = 3;

    private Queue<GameObject> chestPool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = Instantiate(chestPrefab);
            obj.SetActive(false);
            chestPool.Enqueue(obj);
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
            GameObject gate = Instantiate(chestPrefab);
            return gate;
        }
    }

    public void ReturnChest(GameObject gate)
    {
        gate.SetActive(false);
        chestPool.Enqueue(@gate);
    }
}
