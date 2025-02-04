using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody rb;
    private float moveInput;
    private bool isWalking;
    private bool isLeft;

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
            Debug.LogError("Rigidbody를 찾을 수 없습니다.");

        SetPlayerStats(100, 3, 10, 10, 6, 10, 1, 8f, 12f, 4f, 10, 0);
    }


    void Update()
    {
        // A, D 또는 방향키로 좌우 이동 (왼쪽 : -1, 오른쪽 : 1)
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

        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
    }

    public bool isWalk()
    {
        return isWalking;
    }

    public bool isLeftWalk()
    {
        return isLeft;
    }


    public PlayerStats GetPlayerStats()
    {
        return stats;
    }
}
