using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // �÷��̾��� ���� ����
    // �÷��̾��� ��ġ ����? ������ ��ġ ����?


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

    private static List<Sword> ActiveSwords = new List<Sword>();

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
            range = player.GetPlayerStats().SwordRange;
        }
        else
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");

        if (playerPos != null)
        {
            relativePos = _SpawnPos - playerPos.position;
        }

        if (!ActiveSwords.Contains(this))
            ActiveSwords.Add(this);
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

            if (target == null)
            {

            }
        }
    }
}
