using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private Transform leftCol;
    [SerializeField] private Transform rightCol;

    private MonsterPool monsterPool;

    private void Awake()
    {
        monsterPool = FindObjectOfType<MonsterPool>();
        if (monsterPool == null)
            Debug.LogError("monsterPool�� ã�� �� �����ϴ�.");
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

        float randomX = Random.Range(minPos.x, maxPos.x);
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
        float z = minPos.z + 12f;

        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Ÿ�� ��ȣ�� ���� ���� ��ġ
    /// </summary>
    public void SpawnMonster(int tileIdx)
    {
        ClearMonsters();

        Debug.Log("monster��ȯ");
        Vector3 SpawnPos = GetRandomSpawnPos();
        GameObject monster = monsterPool.GetMonster(false);
        monster.transform.position = SpawnPos;
        monster.transform.rotation = Quaternion.LookRotation(-transform.forward);
        monster.transform.SetParent(transform);

        if (tileIdx == 5)
        {
            Debug.Log("���� ��ȯ");
            Vector3 BossSpawnPos = GetBossSpawnPos();
            GameObject Bossmonster = monsterPool.GetMonster(true);
            Bossmonster.transform.position = BossSpawnPos;
            Bossmonster.transform.rotation = Quaternion.LookRotation(-transform.forward);
            Bossmonster.transform.SetParent(transform);
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
}
