using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody rb;
    private float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null )
        {
            Debug.LogError("Rigidbody를 찾을 수 없습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // A, D 또는 방향키로 좌우 이동
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
    }
}
