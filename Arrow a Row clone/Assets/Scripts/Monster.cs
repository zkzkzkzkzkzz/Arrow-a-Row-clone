using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [System.Serializable]
    public struct MonsterStats
    {
        public int HP;

        // ������ ��쿡�� �Ʒ� ���� ���
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
