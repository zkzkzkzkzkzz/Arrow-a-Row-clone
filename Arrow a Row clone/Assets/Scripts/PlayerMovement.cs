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
            Debug.LogError("Rigidbody�� ã�� �� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // A, D �Ǵ� ����Ű�� �¿� �̵�
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
    }
}
