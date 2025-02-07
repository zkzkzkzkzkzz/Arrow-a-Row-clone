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
    /// 맵 타일 경계 내에서 몬스터를 스폰할 랜덤 위치 계산
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomSpawnPos()
    {
        if (leftCol == null || rightCol == null)
        {
            Debug.LogError("MapTile에 경계 콜라이더가 할당되지 않았습니다.");
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
    /// 타일 번호에 따라 몬스터 배치
    /// </summary>
    public void SpawnMonster(int tileIdx)
    {
        Vector3 SpawnPos = GetRandomSpawnPos();
        Instantiate(monster, SpawnPos, Quaternion.identity, transform);
    }
}
