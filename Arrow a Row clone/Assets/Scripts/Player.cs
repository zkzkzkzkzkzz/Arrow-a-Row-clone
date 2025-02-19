using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float moveInput;
    private bool isWalking;
    private bool isLeft;
    private bool isBoard;

    private Animator animator;

    [SerializeField] private SwordBoard swordBoard;

    [SerializeField] List<BowSO> BowList;
    private BowSO curBow;
    private int bowLV = 0;
    private bool isChangeBow = false;

    [SerializeField] private int FinalArrowATK;

    [System.Serializable]
    public struct PlayerStats
    {
        // Player
        public int     HP;
        public float   moveSpeed;

        // Arrow
        public int ArrowATK;
        public int ArrowRate;
        public int ArrowSpeed;
        public int ArrowRange;
        public int ArrowCnt;
         
        // Sword
        public float SwordATK;
        public float SwordRate;
        public float SwordSpeed;
        public int SwordRange;
        public int SwordCnt;

        public int Percentage;
    }

    [System.Serializable]
    public struct ItemStats
    {
        // ������ ����
        public int CritGlassesLV;   // ġ��Ÿ Ȯ�� �� ġ��Ÿ ������ ������
        public int ConvertCapeLV;   // ȭ�� �ӵ��� ���� �κ� �������� ��ȯ
        public int PenetrationLV;   // ȭ�� ���� ����
        public int ShieldLV;        // �÷��̾ �Դ� ������ �Ϻ� ����
        public int LifeStealLV;     // ����� ���

        // ������ ����
        public float CritChance;        // ġ��Ÿ Ȯ��
        public float CritDamage;        // ġ��Ÿ ������ (2f == 200% ������)
        public float ConvertDamage;     // ȭ�� �ӵ� ��ȯ ������
        public float PenetrationDamage; // ����� ȭ���� ������
        public float Shield;            // ������ ���ҷ�
        public float LifeSteal;         // ����� ��� ����
    }

    [SerializeField] private PlayerStats stats;
    [SerializeField] private ItemStats itemStats;

    public void SetPlayerStats(int _HP, int _moveSpeed,
                        int _ArrowATK, int _ArrowRate, int _ArrowSpeed, int _ArrowRange, int _ArrowCnt,
                        float _SwordATK, float _SwordRate, float _SwordSpeed, int _SwordRange, int _SwordCnt, int _Percentage)
    {
        stats.HP = _HP;
        stats.moveSpeed = _moveSpeed;
        
        stats.ArrowATK = _ArrowATK;
        stats.ArrowRate = _ArrowRate;
        stats.ArrowSpeed = _ArrowSpeed;
        stats.ArrowRange = _ArrowRange;
        stats.ArrowCnt = _ArrowCnt;

        stats.SwordATK = _SwordATK;
        stats.SwordRate = _SwordRate;
        stats.SwordSpeed = _SwordSpeed;
        stats.SwordRange = _SwordRange;
        stats.SwordCnt = _SwordCnt;

        stats.Percentage = _Percentage;
    }

    public void SetItemStats(int _CritGlassesLV, int _ConvertCapeLV, int _PenetrationLV, int _ShieldLV, int _LifeStealLV,
                            float _CritChance, float _CritDamage, float _Penetration, float _Shield, float _LifeSteal)
    {
        itemStats.CritGlassesLV = _CritGlassesLV;
        itemStats.ConvertCapeLV = _ConvertCapeLV;
        itemStats.PenetrationLV = _PenetrationLV;
        itemStats.ShieldLV = _ShieldLV;
        itemStats.LifeStealLV = _LifeStealLV;

        itemStats.CritChance = _CritChance;
        itemStats.CritDamage = _CritDamage;
        itemStats.PenetrationDamage = _Penetration;
        itemStats.Shield = _Shield;
        itemStats.LifeSteal = _LifeSteal;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("Rigidbody�� ã�� �� �����ϴ�.");

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("�ִϸ����͸� ã�� �� �����ϴ�.");
        }

        if (BowList.Count > 0)
        {
            curBow = BowList[0];
            ApplyBowStats();
        }

        CalFinalArrowATK();
    }


    void Update()
    {
        // A, D �Ǵ� ����Ű�� �¿� �̵� (���� : -1, ������ : 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        isWalking = moveInput != 0;

        if (isWalking)
            isLeft = moveInput < 0;
        else
            isLeft = false;

        isChangeBow = false;
    }

    void FixedUpdate()
    {
        if (rb == null)
            return;

        rb.velocity = new Vector3(moveInput * stats.moveSpeed, rb.velocity.y, 0);
    }

    public bool isWalk()
    {
        return isWalking;
    }

    public bool isLeftWalk()
    {
        return isLeft;
    }

    public void SetOnBoard(bool b)
    {
        isBoard = b;
        animator.SetBool("isBoard", isBoard);
    }

    public bool isOnBoard()
    {
        return isBoard;
    }

    public PlayerStats GetPlayerStats()
    {
        return stats;
    }

    public ItemStats GetPlayerItemStats()
    {
        return itemStats;
    }

    public float GetArrowFireRate()
    {
        return 10f / stats.ArrowRate;
    }

    public void TakeDamage(int damage)
    {
        stats.HP -= damage;
    }

    public void IncreaseStat(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.HP:
                stats.HP += (int)value;
                break;
            case StatType.MOVESPEED:
                stats.moveSpeed += value;
                break;
            case StatType.ARROWATK:
                stats.ArrowATK += (int)value;
                break;
            case StatType.ARROWRATE:
                stats.ArrowRate += (int)value;
                if (stats.ArrowRate <= 0)
                    stats.ArrowRate = 1;
                break;
            case StatType.ARROWSPEED:
                stats.ArrowSpeed += (int)value;
                break;
            case StatType.ARROWRANGE:
                stats.ArrowRange += (int)value;
                break;
            case StatType.ARROWCNT:
                stats.ArrowCnt += (int)value;
                break;
            case StatType.SWORDATK:
                stats.SwordATK += value;
                break;
            case StatType.SWORDRATE:
                stats.SwordRate *= (1 - (value / 100f));
                break;
            case StatType.SWORDSPEED:
                stats.SwordSpeed += value;
                break;
            case StatType.SWORDRANGE:
                stats.SwordRange += (int)value;
                break;
            case StatType.SWORDCNT:
                stats.SwordCnt += (int)value;
                break;
            case StatType.PERCENTAGE:
                stats.Percentage = Mathf.Clamp((int)value, 0, 100);
                break;
            default:
                break;
        }

        CalFinalArrowATK();
    }

    public void IncreaseItemStats(ItemType type, float value)
    {
        switch (type)
        {
            case ItemType.CRITGLASSESLV:
                itemStats.CritGlassesLV += (int)value;
                break;
            case ItemType.CONVERTCAPELV:
                itemStats.ConvertCapeLV += (int)value;
                break;
            case ItemType.PENETRATIONLV:
                itemStats.PenetrationLV += (int)value;
                break;
            case ItemType.SHIELDLV:
                itemStats.ShieldLV += (int)value;
                break;
            case ItemType.LIFESTEALLV:
                itemStats.LifeStealLV += (int)value;
                break;

            case ItemType.CRITCHANCE:
                itemStats.CritChance = value;
                break;
            case ItemType.CRITDAMAGE:
                itemStats.CritDamage = value;
                break;
            case ItemType.CONVERTDAMAGE:
                itemStats.ConvertDamage = value;
                break;
            case ItemType.PENETRATIONDAMAGE:
                itemStats.PenetrationDamage = value;
                break;
            case ItemType.SHIELD:
                itemStats.Shield = value;
                break;
            case ItemType.LIFESTEAL:
                itemStats.LifeSteal = value;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Ȱ ���� ���� �Լ�
    /// </summary>
    public void SelectBow(BowSO selectedBow)
    {
        curBow = selectedBow;
        bowLV = 1;
    }

    public void UpgradeBow(BowSO nextBow)
    {
        curBow = nextBow;
        ++bowLV;
    }

    private void ApplyBowStats()
    {
        if (curBow.StatBonusList.Count > 0)
        {
            // ������ 1���� ���������� ���Ⱥ��ʽ�����Ʈ �ε����� 0���� ����
            foreach (var stat in curBow.StatBonusList[bowLV - 1])
                IncreaseStat(stat.statType, stat.value);
        }
    }

    public Color GetCurBowColor()
    {
        return curBow.color;
    }

    public bool IsChangeBow()
    {
        return isChangeBow;
    }

    public int GetFinalArrowATK()
    {
        return FinalArrowATK;
    }

    private void CalFinalArrowATK()
    {
        FinalArrowATK = Mathf.RoundToInt(stats.ArrowATK * (stats.Percentage / 100f));

        if (IsCritical())
            FinalArrowATK = Mathf.RoundToInt(FinalArrowATK * itemStats.CritDamage);
    }

    private bool IsCritical()
    {
        float val = Random.Range(0f, 100f);
        
        return val >= itemStats.CritChance ? true : false;
    }


    /// <summary>
    /// ����׿� Ȱ ����
    /// </summary>
    public void ChangeBow()
    {
        int curIdx = BowList.IndexOf(curBow);
        int nextIdx = (curIdx + 1) % BowList.Count;
        curBow = BowList[nextIdx];

        bowLV = 1;
        isChangeBow = true;
        ApplyBowStats();

        Debug.Log($" Ȱ ����: {curBow.bowName} (Lv. {bowLV})");
    }

    public void ChangeBowLV(int lv)
    {
        bowLV = Mathf.Clamp(bowLV + lv, 1, 4);
        ApplyBowStats();

        Debug.Log($" Ȱ ���� ����: {curBow.bowName} (Lv. {bowLV})");
    }

    public void GainCritGlasses()
    {
        itemStats.CritGlassesLV = Mathf.Clamp(++itemStats.CritGlassesLV, 0, 4);
    }
}
