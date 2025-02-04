using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator animator;
    private const string IS_WALK = "isWalk";
    private const string IS_LEFT = "isLeft";
    private const string IS_SHOOT = "isShoot";

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("animator를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        animator.SetBool(IS_WALK, player.isWalk());
        animator.SetBool(IS_LEFT, player.isLeftWalk());
        animator.SetBool(IS_SHOOT, true);
    }
}
