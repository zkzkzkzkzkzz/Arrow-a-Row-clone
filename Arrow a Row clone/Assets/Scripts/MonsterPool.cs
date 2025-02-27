using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private GameObject monsterPref;
    [SerializeField] private int monsterPoolSize = 5;
    [SerializeField] private GameObject bossPref;
    [SerializeField] private int bossPoolSize = 2;
    [SerializeField] private GameObject finalBossPref;
    [SerializeField] private int finalBossPoolSize = 1;

    [SerializeField] private Transform poolContainer;

    private Queue<GameObject> monsterPool = new Queue<GameObject>();
    private Queue<GameObject> bossPool = new Queue<GameObject>();
    private Queue<GameObject> finalBossPool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < monsterPoolSize; ++i)
        {
            GameObject monster = Instantiate(monsterPref);
            monster.SetActive(false);
            monsterPool.Enqueue(monster);
            monster.transform.SetParent(poolContainer);
        }

        for (int i = 0; i < bossPoolSize; ++i)
        {
            GameObject boss = Instantiate(bossPref);
            boss.SetActive(false);
            bossPool.Enqueue(boss);
            boss.transform.SetParent(poolContainer);
        }

        for (int i = 0; i < finalBossPoolSize; ++i)
        {
            GameObject finalBoss = Instantiate(finalBossPref);
            finalBoss.SetActive(false);
            finalBossPool.Enqueue(finalBoss);
            finalBoss.transform.SetParent(poolContainer);
        }
    }

    public GameObject GetMonster(bool _isBoss)
    {
        if (_isBoss)
        {
            GameObject boss = null;
            if (bossPool.Count > 0)
                boss = bossPool.Dequeue();
            else
                boss = Instantiate(bossPref);
            
            boss.SetActive(true);
            return boss;
        }
        else
        {
            GameObject monster = null;
            if (monsterPool.Count > 0)
                monster = monsterPool.Dequeue();
            else
                monster = Instantiate(monsterPref);

            monster.SetActive(true);
            return monster;
        }
    }

    public void ReturnMonster(GameObject monster, bool _isBoss)
    {
        monster.transform.SetParent(poolContainer);
        monster.SetActive(false);

        if (_isBoss)
            bossPool.Enqueue(monster);
        else
            monsterPool.Enqueue(monster);
    }

    public GameObject GetFinalBoss()
    {
        GameObject finalBoss = null;
        if (finalBossPool.Count > 0)
            finalBoss = finalBossPool.Dequeue();
        else
            finalBoss = Instantiate(finalBossPref);

        finalBoss.SetActive(true);
        return finalBoss;
    }

    public void ReturnFinalBoss(GameObject boss)
    {
        boss.transform.SetParent(poolContainer);
        boss.SetActive(false);
        finalBossPool.Enqueue(boss);
    }
}
