using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private Transform leftCol;
    [SerializeField] private Transform rightCol;

    private MonsterPool monsterPool;
    private GateSpawner gateSpawner;

    private void Awake()
    {
        monsterPool = FindObjectOfType<MonsterPool>();
        if (monsterPool == null)
            Debug.LogError("monsterPool�� ã�� �� �����ϴ�.");

        gateSpawner = GetComponentInChildren<GateSpawner>();
        if (gateSpawner == null)
            Debug.LogError("mapTile���� gateSpawner�� ã�� �� �����ϴ�.");
    }


    /// <summary>
    /// �� Ÿ�� ��� ������ ���͸� ������ ���� ��ġ ���
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomSpawnPos()
    {
        if (leftCol == null || rightCol == null)
        {
            Debug.LogError("MapTile�� ��� �ݶ��̴��� �Ҵ���� �ʾҽ��ϴ�.");
            return transform.position;
        }

        Vector3 minPos = leftCol.position;
        Vector3 maxPos = rightCol.position;

        float offset = 1f;

        float randomX = Random.Range(minPos.x + offset, maxPos.x - offset);
        float y = -0.3f;
        float randomZ = Random.Range(minPos.z, maxPos.z);

        return new Vector3(randomX, y, randomZ);
    }

    private Vector3 GetBossSpawnPos()
    {
        Vector3 minPos = leftCol.position;
        Vector3 maxPos = rightCol.position;

        float x = 0f;
        float y = -0.3f;
        float z = minPos.z + 6f;

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Ÿ�� ��ȣ�� ���� ���� ��ġ
    /// </summary>
    public void SpawnMonster(int tileIdx, int chapter)
    {
        CheckSpawnChest();
        ClearMonsters(); // ���� Ÿ�Ͽ� ���Ͱ� �������� ��� Ǯ�� ��ȯ

        // ���� ��ȯ
        Vector3 SpawnPos = GetRandomSpawnPos();
        GameObject monster = monsterPool.GetMonster(false);
        monster.transform.position = SpawnPos;
        monster.transform.rotation = Quaternion.LookRotation(-transform.forward);
        monster.transform.SetParent(transform);

        int hp = CalculateMonsterHP(tileIdx, chapter, false);
        monster.GetComponent<Monster>().SetMonsterHP(hp);

        if (tileIdx == 5)
        {
            // ���� ��ȯ
            Vector3 BossSpawnPos = GetBossSpawnPos();
            GameObject Bossmonster = monsterPool.GetMonster(true);
            Bossmonster.transform.position = BossSpawnPos;
            Bossmonster.transform.rotation = Quaternion.LookRotation(-transform.forward);
            Bossmonster.transform.SetParent(transform);
            
            int bossHp = CalculateMonsterHP(tileIdx, chapter, true);
            Bossmonster.GetComponent<Monster>().SetMonsterHP(bossHp);
        }
    }

    private void ClearMonsters()
    {
        List<Transform> monsters = new List<Transform>();
        List<Transform> boss = new List<Transform>();

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Enemy"))
                monsters.Add(t);
            else if (t.CompareTag("Boss"))
                boss.Add(t);
        }

        for (int i = 0; i < monsters.Count; ++i)
        {
            monsterPool.ReturnMonster(monsters[i].gameObject, false);
        }
        
        
        for (int i = 0; i < boss.Count; ++i)
        {
            monsterPool.ReturnMonster(boss[i].gameObject, true);
        }
    }

    private int CalculateMonsterHP(int tileIdx, int chapter, bool isBoss)
    {
        int baseHP = 50;

        if (isBoss)
            return baseHP + (chapter - 1) * tileIdx + 25 * tileIdx;
        else
            return baseHP + (chapter - 1) * tileIdx + 10 * tileIdx;
    }

    public GateSpawner GetGateSpawner()
    {
        return gateSpawner;
    }

    /// <summary>
    /// ���� �����ִ� ������ �������ڰ� �ִ��� Ȯ���ϰ� �����ִٸ� Ǯ�� ��ȯ
    /// </summary>
    private void CheckSpawnChest()
    {
        List<Transform> chest = new List<Transform>();
        ChestPool chestPool = FindObjectOfType<ChestPool>();

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Chest"))
            {
                Debug.Log("���� �߰�");
                chest.Add(t);
            }
        }

        for (int i = 0; i < chest.Count; ++i)
        {
            chestPool.ReturnChest(chest[i].gameObject);
        }
    }
}
