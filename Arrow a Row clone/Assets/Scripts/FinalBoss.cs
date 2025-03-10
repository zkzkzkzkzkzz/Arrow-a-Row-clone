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
            Debug.LogError("FinalBoss���� monsterPool�� ã�� �� �����ϴ�.");

        chestPool = FindObjectOfType<ChestPool>();
        if (chestPool == null)
            Debug.LogError("FinalBoss���� chestPool�� ã�� �� �����ϴ�.");

        HPText = GetComponentInChildren<TextMeshPro>();
    }

    private void OnEnable()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            Debug.LogError("FinalBoss���� player�� ã�� �� �����ϴ�.");
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
            Debug.Log("���� ���� óġ");
            GameManager.Instance.EndGame();
        }
        else
            SetFinalBossHP(curHP - damage);
    }
}
