using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBoard : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRadius = 2f;

    private Player player;
    private ObjPool objPool;

    private float nextSpawnTime = 0f;
    private bool isSpawn = false;

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
        //if (!player.isOnBoard())
        //    return;

        nextSpawnTime += Time.deltaTime;
        if (nextSpawnTime >= player.GetPlayerStats().SwordRate && !isSpawn)
        {
            nextSpawnTime = 0f;
            SpawnSword();
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
        }
    }
}
