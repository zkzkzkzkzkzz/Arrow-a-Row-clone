using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoard : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private Player player;
    private ObjPool objPool;

    private float nextSpawnTime = 0f;
    private bool isSpawn = false;
    private float range;

    private List<Sword> ActiveSwords = new List<Sword>();

    private void Start()
    {
        player = GetComponentInParent<Player>();
        if (player == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }

        objPool = FindObjectOfType<ObjPool>();
        if (objPool == null)
        {
            Debug.LogError("arrowPool을 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        if (!player.isOnBoard())
            return;

        if (!isSpawn)
           nextSpawnTime += Time.deltaTime;
         
        if (nextSpawnTime >= player.GetPlayerStats().SwordRate && !isSpawn)
        {
            nextSpawnTime = 0f;
            SpawnSword();
        }

        if (isSpawn)
        {
            range = player.GetPlayerStats().SwordRange;
            DetectEnemy();
        }

        ActiveSwords.RemoveAll(sword => sword == null || !sword.gameObject.activeSelf);

        if (ActiveSwords.Count == 0)
            isSpawn = false;
    }





    /// <summary>
    /// 플레이어 중심으로 감지 범위 내 적들을 검사하여 가장 가까운 적을 선택한 후,
    /// Sword 클래스에 타겟으로 전달
    /// </summary>
    private void DetectEnemy()
    {
        Vector3 pos = player.transform.position;
        Collider[] hits = Physics.OverlapSphere(pos, range);

        Transform enemy = null;
        float minDist = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
            {
                float dist = Vector3.Distance(pos, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    enemy = hit.transform;
                }
            }
        }

        for (int i = 0; i < ActiveSwords.Count; ++i)
        {
            ActiveSwords[i].SetTarget(enemy);
        }
    }


    private void SpawnSword()
    {
        isSpawn = true;
        int swordCnt = player.GetPlayerStats().SwordCnt;
        float swordRange = player.GetPlayerStats().SwordRange;

        for (int i = 0; i < swordCnt; ++i)
        {
            GameObject sword = objPool.GetSword();

            Vector2 randomOffset = Random.insideUnitCircle;
            Vector3 spawnPos = spawnPoint.position + new Vector3(randomOffset.x, 0, -randomOffset.y);

            sword.transform.position = spawnPos;

            Sword swordScript = sword.GetComponent<Sword>();
            if (swordScript != null)
            {
                swordScript.Initialize(spawnPos);
            }

            ActiveSwords.Add(swordScript);
        }
    }
}
