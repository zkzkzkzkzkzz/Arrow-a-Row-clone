using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private Player player;
    private MonsterPool monsterPool;

    private ChestPool chestPool;
    private TextMeshPro HPText;

    [System.Serializable]
    public struct BossStats
    {
        public int HP;
        public int Damage;
    }

    [SerializeField] private BossStats stats;

    private void Start()
    {
        monsterPool = FindObjectOfType<MonsterPool>();
        if (monsterPool == null)
            Debug.LogError("FinalBoss에서 monsterPool을 찾을 수 없습니다.");

        chestPool = FindObjectOfType<ChestPool>();
        if (chestPool == null)
            Debug.LogError("FinalBoss에서 chestPool을 찾을 수 없습니다.");

        HPText = GetComponentInChildren<TextMeshPro>();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            Debug.LogError("FinalBoss에서 player를 찾을 수 없습니다.");
    }

    private void Update()
    {
        HPText.text = stats.HP.ToString();
    }

    public void SetFinalBossHP(int hp)
    {
        stats.HP = hp;
    }

    public int GetFinalBossHP()
    {
        return stats.HP;
    }

    public void TakeDamage(int damage)
    {
        int curHP = GetFinalBossHP();
        if (curHP - damage <= 0)
        {
            Transform tempPos = transform.parent;
            monsterPool.ReturnFinalBoss(gameObject);
            SpawnChest(tempPos);

            MapTileMgr mapTileMgr = FindObjectOfType<MapTileMgr>();
            if (mapTileMgr != null)
                mapTileMgr.OnFinalBossDefeated();
        }
        else
            SetFinalBossHP(curHP - damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int damage = GetFinalBossHP();

            if (player.HasShield())
            {
                damage = Mathf.RoundToInt(damage * (1 - (player.GetPlayerItemStats().Shield / 100)));
            }

            player.TakeDamage(damage);
            Transform tempPos = transform.parent;
            monsterPool.ReturnFinalBoss(gameObject);
            SpawnChest(tempPos);
        }
    }

    private void SpawnChest(Transform parent)
    {
        Vector3 spawnPos = transform.position;
        GameObject chest = chestPool.GetBossChest();
        chest.transform.position = new Vector3(spawnPos.x, spawnPos.y + 0.7f, spawnPos.z);
        chest.transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        chest.transform.SetParent(parent);

        chest.GetComponent<BossChest>().IdleBossChest();
    }
}
