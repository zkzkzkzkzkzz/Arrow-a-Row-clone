using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // ȭ�� �߻� ��ġ
    private Player player;
    private ArrowPool arrowPool;

    private float nextFireTime;

    void Start()
    {
        player = GetComponentInParent<Player>();

        if (player == null )
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }

        arrowPool = FindObjectOfType<ArrowPool>();

        if ( arrowPool == null )
        {
            Debug.LogError("arrowPool�� ã�� �� �����ϴ�.");
        }

        nextFireTime = 0f;
    }

    void Update()
    {
        nextFireTime += Time.deltaTime;

        if (nextFireTime >= player.GetArrowFireRate())
        {
            nextFireTime = 0f;
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        GameObject arrow = arrowPool.GetArrow();
        arrow.transform.position = firePoint.position;

        arrow.transform.rotation = Quaternion.Euler(90, 0, 0);

        Arrow arrowScript = arrow.GetComponent<Arrow>();
        if ( arrowScript != null )
        {
            arrowScript.SetStartPos(firePoint.position);
        }

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * player.GetPlayerStats().ArrowSpeed;
    }
}
