using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePool : MonoBehaviour
{
    [SerializeField] private GameObject gatePref;
    [SerializeField] private int poolSize = 6;

    private Queue<GameObject> gatePool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = Instantiate(gatePref);
            obj.SetActive(false);
            gatePool.Enqueue(obj);
        }
    }

    public GameObject GetGate()
    {
        if (gatePool.Count > 0)
        {
            GameObject @object = gatePool.Dequeue();
            @object.SetActive(true);
            return @object;
        }
        else
        {
            GameObject gate = Instantiate(gatePref);
            return gate;
        }
    }

    public void ReturnGate(GameObject gate)
    {
        gate.SetActive(false);
        gatePool.Enqueue(@gate);
    }
}
