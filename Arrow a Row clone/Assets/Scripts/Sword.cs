using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // ���� : �Ϲ�, ����
    private enum SwordState { IDLE, TRACE };
    private SwordState swordState;
    private Vector3 startPos;
    private Vector3 relativePos;

    private Player player;
    private Transform playerPos;
    private Transform target;
    private ObjPool objPool;

    // �� ���� (�÷��̾�κ��� ����)
    private float speed;
    private float damage;
    private float range;
    
    /// <summary>
    /// SwordBoard���� ���� ������ �� ȣ���ϴ� �Լ�
    /// </summary>
    public void Initialize(Vector3 _SpawnPos)
    {
        this.startPos = _SpawnPos;
        transform.position = _SpawnPos;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        swordState = SwordState.IDLE;

        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerPos = player.transform;
            speed = player.GetPlayerStats().SwordSpeed;
            damage = player.GetPlayerStats().SwordATK;
        }
        else
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");

        if (playerPos != null)
        {
            relativePos = _SpawnPos - playerPos.position;
        }
    }

    private void Awake()
    {
        objPool = FindObjectOfType<ObjPool>();
        if (objPool == null)
            Debug.LogError("objPool�� ã�� �� �����ϴ�.");
    }

    private void Update()
    {
        if (swordState == SwordState.IDLE)
        {
            if (playerPos != null)
            {
                transform.position = playerPos.position + relativePos;
            }

            if (target != null)
            {
                swordState = SwordState.TRACE;
            }

            if (target != null && !target.gameObject.activeInHierarchy)
            {
                objPool.ReturnSword(gameObject);
                return;
            }
        }
        else
        {
            if (target == null || !target.gameObject.activeInHierarchy)
            {
                objPool.ReturnSword(gameObject);
                return;
            }

            Vector3 Dir = (target.position - transform.position).normalized;
            Quaternion targetRot = Quaternion.LookRotation(Dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 360f * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("FinalBoss"))
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
                other.GetComponent<Monster>().TakeDamage((int)player.GetPlayerStats().SwordATK);
            else
                other.GetComponent<FinalBoss>().TakeDamage((int)player.GetPlayerStats().SwordATK);
            objPool.ReturnSword(gameObject);
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
