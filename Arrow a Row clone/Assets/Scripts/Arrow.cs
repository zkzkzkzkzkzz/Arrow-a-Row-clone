using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;
    private ArrowPool arrowPool;

    private Vector3 startPos; // 발사되는 순간의 위치
    private float range;

    void Start()
    {
        arrowPool = FindObjectOfType<ArrowPool>();
    }

    void OnEnable()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            range = player.GetPlayerStats().ArrowRange;
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= range)
        {
            Debug.Log("최대 사정거리");
            arrowPool.ReturnArrow(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "과 충돌");
        arrowPool.ReturnArrow(gameObject);
    }

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }
}
