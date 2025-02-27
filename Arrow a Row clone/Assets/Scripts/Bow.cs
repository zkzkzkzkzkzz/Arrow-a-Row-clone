using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // ȭ�� �߻� ��ġ
    private Player player;
    private ObjPool arrowPool;

    private float nextFireTime;

    private AudioSource audioSource;

    void Start()
    {
        player = GetComponentInParent<Player>();

        if (player == null )
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }

        arrowPool = FindObjectOfType<ObjPool>();

        if ( arrowPool == null )
        {
            Debug.LogError("arrowPool�� ã�� �� �����ϴ�.");
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
    /// Ȱ ���׷��̵� �� ���׸��� ����
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
