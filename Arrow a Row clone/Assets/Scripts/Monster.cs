using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
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
}
