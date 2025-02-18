using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    private BowSO curBow;
    private int bowLV = 0;

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
    }

    [SerializeField] private PlayerStats stats;

    public void SetPlayerStats(int _HP, int _moveSpeed,
                        int _ArrowATK, int _ArrowRate, int _ArrowSpeed, int _ArrowRange, int _ArrowCnt,
                        float _SwordATK, float _SwordRate, float _SwordSpeed, int _SwordRange, int _SwordCnt)
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

        //Debug.Log($"isWalking : {isWalking}, isLeft : {isLeft}");
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

    public float GetArrowFireRate()
    {
        return 10f / stats.ArrowRate;
    }

    public void TakeDamage(int damage)
    {
        stats.HP -= damage;
    }

    public void increaseStat(StatType statType, float value)
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
        if (curBow.upgradeStats.ContainsKey(bowLV))
        {
            foreach (var stat in curBow.upgradeStats[bowLV])
                increaseStat(stat.statType, stat.value);
        }
    }
}
