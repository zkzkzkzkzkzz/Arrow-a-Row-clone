using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private GameObject monsterPref;
    [SerializeField] private int monsterPoolSize = 5;
    [SerializeField] private GameObject bossPref;
    [SerializeField] private int bossPoolSize = 2;

    private Queue<GameObject> monsterPool = new Queue<GameObject>();
    private Queue<GameObject> bossPool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < monsterPoolSize; ++i)
        {
            GameObject monster = Instantiate(monsterPref);
            monster.SetActive(false);
            monsterPool.Enqueue(monster);
        }

        for (int i = 0; i < bossPoolSize; ++i)
        {
            GameObject boss = Instantiate(bossPref);
            boss.SetActive(false);
            bossPool.Enqueue(boss);
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
        monster.SetActive(false);

        if (_isBoss)
            bossPool.Enqueue(monster);
        else
            monsterPool.Enqueue(monster);
    }
}
