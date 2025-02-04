using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    [SerializeField] private GameObject arrowPref;
    [SerializeField] private int poolSize = 15;

    [SerializeField] private GameObject swordPref;
    [SerializeField] private int poolSize2 = 15;

    private Queue<GameObject> arrowPool = new Queue<GameObject>();
    private Queue<GameObject> swordPool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject arrow = Instantiate(arrowPref);
            arrow.SetActive(false);
            arrowPool.Enqueue(arrow);
        }

        for (int i = 0; i < poolSize2; ++i)
        {
            GameObject sword = Instantiate(swordPref);
            sword.SetActive(false);
            swordPool.Enqueue(sword);
        }
    }

    public GameObject GetArrow()
    {
        if (arrowPool.Count > 0)
        {
            GameObject arrow =  arrowPool.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }
        else
        {
            GameObject arrow = Instantiate(arrowPref);
            return arrow;
        }
    }

    public GameObject GetSword()
    {
        if (swordPool.Count > 0)
        {
            GameObject sword = swordPool.Dequeue();
            sword.SetActive(true);
            return sword;
        }
        else
        {
            GameObject sword = Instantiate(swordPref);
            return sword;
        }
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrowPool.Enqueue(arrow);
    }

    public void ReturnSword(GameObject sword)
    {
        sword.SetActive(false);
        swordPool.Enqueue(sword);
    }
}
