using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;
    private ObjPool objPool;

    private Vector3 startPos; // 발사되는 순간의 위치
    private float range;

    void Start()
    {
        objPool = FindObjectOfType<ObjPool>();
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
            objPool.ReturnArrow(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            other.GetComponent<Monster>().TakeDamage(player.GetPlayerStats().ArrowATK);
            objPool.ReturnArrow(gameObject);
        }
        else
        {
            return;
        }
    }

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }
}
