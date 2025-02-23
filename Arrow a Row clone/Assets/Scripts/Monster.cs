using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player player;
    private MonsterPool monsterPool;

    private ChestPool chestPool;
    private TextMeshPro HPText;

    private void Start()
    {
        monsterPool = FindObjectOfType<MonsterPool>();
        if (monsterPool == null)
            Debug.LogError("Monster에서 monsterPool을 찾을 수 없습니다.");

        chestPool = FindObjectOfType<ChestPool>();
        if (chestPool == null)
            Debug.LogError("Monster에서 chestPool을 찾을 수 없습니다.");

        HPText = GetComponentInChildren<TextMeshPro>();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            Debug.LogError("Monster에서 player를 찾을 수 없습니다.");
    }

    private void Update()
    {
        HPText.text = stats.HP.ToString();
    }

    [System.Serializable]
    public struct MonsterStats
    {
        public int HP;

        // 보스일 경우에만 아래 스탯 사용
        public bool isBoss;
        public int Damage;
    }

    [SerializeField] private MonsterStats stats;

    public void SetMonsterHP(int hp)
    {
        stats.HP = hp;
    }

    public int GetMonsterHP()
    {
        return stats.HP;
    }

    public bool IsBoss()
    {
        return stats.isBoss;
    }

    public void TakeDamage(int damage)
    {
        int curHP = GetMonsterHP();
        if (curHP - damage <= 0)
        {
            Transform tempPos = transform.parent;
            monsterPool.ReturnMonster(gameObject, stats.isBoss);
            SpawnChest(tempPos);
        }
        else
            SetMonsterHP(curHP - damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int damage = GetMonsterHP();

            if (player.HasShield())
            {
                damage = Mathf.RoundToInt(damage * (1 - (player.GetPlayerItemStats().Shield / 100)));
            }

            player.TakeDamage(damage);
            Transform tempPos = transform.parent;
            monsterPool.ReturnMonster(gameObject, stats.isBoss);
            SpawnChest(tempPos);
        }
    }

    private void SpawnChest(Transform parent)
    {
        if (IsBoss())
        {
            Vector3 spawnPos = transform.position;
            GameObject chest = chestPool.GetBossChest();
            chest.transform.position = new Vector3(spawnPos.x, spawnPos.y + 0.7f, spawnPos.z);
            chest.transform.rotation = Quaternion.LookRotation(-Vector3.forward);
            chest.transform.SetParent(parent);

            chest.GetComponent<BossChest>().IdleBossChest();
        }
        else
        {
            Vector3 spawnPos = transform.position;
            GameObject chest = chestPool.GetChest();
            chest.transform.position = new Vector3(spawnPos.x, spawnPos.y + 0.4f, spawnPos.z);
            chest.transform.rotation = Quaternion.LookRotation(-Vector3.forward);
            chest.transform.SetParent(parent);

            chest.GetComponent<Chest>().IdleChest();
        }
    }
}
