using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player player;
    private MonsterPool monsterPool;

    private void Start()
    {
        monsterPool = FindObjectOfType<MonsterPool>();
        if (monsterPool == null)
            Debug.LogError("Monster에서 monsterPool을 찾을 수 없습니다.");
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            Debug.LogError("Monster에서 player를 찾을 수 없습니다.");
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

    public void TakeDamage(int damage)
    {
        int curHP = GetMonsterHP();
        if (curHP - damage <= 0)
            monsterPool.ReturnMonster(gameObject, stats.isBoss);
        else
            SetMonsterHP(curHP - damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int damage = GetMonsterHP();
            player.TakeDamage(damage);
            monsterPool.ReturnMonster(gameObject, stats.isBoss);
        }
    }
}
