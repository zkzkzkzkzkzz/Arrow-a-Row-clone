using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // 플레이어의 스탯 정보
    // 플레이어의 위치 정보? 보드의 위치 정보?


    // 상태 : 일반, 추적
    private enum SwordState { IDLE, TRACE };
    private SwordState swordState;
    private Vector3 startPos;
    private Vector3 relativePos;

    private Player player;
    private Transform playerPos;
    private Transform target;
    private ObjPool objPool;

    // 검 스탯 (플레이어로부터 참조)
    private float speed;
    private float damage;
    private float range;

    private static List<Sword> ActiveSwords = new List<Sword>();

    /// <summary>
    /// SwordBoard에서 검을 스폰할 때 호출하는 함수
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
            Debug.LogError("플레이어를 찾을 수 없습니다.");

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
            Debug.LogError("objPool을 찾을 수 없습니다.");
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
