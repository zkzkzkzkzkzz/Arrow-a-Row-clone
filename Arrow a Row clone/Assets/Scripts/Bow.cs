using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // 화살 발사 위치
    private Player player;
    private ArrowPool arrowPool;

    private float nextFireTime;

    void Start()
    {
        player = GetComponentInParent<Player>();
        arrowPool = FindObjectOfType<ArrowPool>();

        nextFireTime = Time.time + player.GetArrowFireRate();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootArrow();
            nextFireTime = Time.time + player.GetArrowFireRate();
        }
    }

    void ShootArrow()
    {
        GameObject arrow = arrowPool.GetArrow();
        arrow.transform.position = firePoint.position;

        arrow.transform.rotation = Quaternion.Euler(90, 0, 0);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * player.GetPlayerStats().ArrowSpeed;
    }
}
