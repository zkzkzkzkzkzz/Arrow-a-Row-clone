using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject boss;

    [SerializeField] private Transform leftCol;
    [SerializeField] private Transform rightCol;

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
        float y = minPos.y;
        float randomZ = Random.Range(minPos.z, maxPos.z);

        return new Vector3(randomX, y, randomZ);
    }

    /// <summary>
    /// Ÿ�� ��ȣ�� ���� ���� ��ġ
    /// </summary>
    public void SpawnMonster(int tileIdx)
    {
        Vector3 SpawnPos = GetRandomSpawnPos();
        Instantiate(monster, SpawnPos, Quaternion.identity, transform);
    }
}
