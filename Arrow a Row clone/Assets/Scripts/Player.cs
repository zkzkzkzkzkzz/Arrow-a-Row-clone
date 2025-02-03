using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody rb;
    private float moveInput;
    private bool isWalking;
    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("Rigidbody를 찾을 수 없습니다.");
    }

    // Update is called once per frame
    void Update()
    {
        // A, D 또는 방향키로 좌우 이동 (왼쪽 : -1, 오른쪽 : 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        isWalking = moveInput != 0;

        if (isWalking)
            isLeft = moveInput < 0;
        else
            isLeft = false;

        Debug.Log($"isWalking : {isWalking}, isLeft : {isLeft}");
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
}
