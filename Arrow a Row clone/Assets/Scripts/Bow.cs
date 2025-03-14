using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // 화살 발사 위치
    private Player player;
    private ObjPool arrowPool;

    private float nextFireTime;

    private AudioSource audioSource;

    void Start()
    {
        player = GetComponentInParent<Player>();

        if (player == null )
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }

        arrowPool = FindObjectOfType<ObjPool>();

        if ( arrowPool == null )
        {
            Debug.LogError("arrowPool을 찾을 수 없습니다.");
        }

        nextFireTime = 0f;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        nextFireTime += Time.deltaTime;

        if (nextFireTime >= player.GetArrowFireRate())
        {
            nextFireTime = 0f;
            ShootArrow();
            if (audioSource != null)
                audioSource.Play();
        }

        if (player.IsChangeBow())
            ChangeBowMaterial();
    }

    void ShootArrow()
    {
        int arrowCnt = player.GetPlayerStats().ArrowCnt;
        float range = player.GetPlayerStats().ArrowRange;

        float spreadAngle = Mathf.Lerp(0.3f, 3f, arrowCnt / range);
        float startAngle = -spreadAngle * (arrowCnt - 1) / 2;

        for (int i = 0; i < arrowCnt; ++i)
        {
            GameObject arrow = arrowPool.GetArrow();
            arrow.transform.position = firePoint.position;

            float angle = startAngle + (spreadAngle * i);
            arrow.transform.rotation = Quaternion.Euler(90, angle, 0);

            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if ( arrowScript != null )
            {
                arrowScript.SetStartPos(firePoint.position);
            }

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.velocity = Quaternion.Euler(0, angle, 0) * Vector3.forward * player.GetPlayerStats().ArrowSpeed;
        }
    }

    /// <summary>
    /// 활 업그레이드 시 머테리얼 변경
    /// </summary>
    private void ChangeBowMaterial()
    {
        Color color = player.GetCurBowColor();

        Renderer rend = GetComponentInChildren<Renderer>();

        if (rend != null)
            rend.material.color = color;

        player.isChangeBow = false;
    }
}
