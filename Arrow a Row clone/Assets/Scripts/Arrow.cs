using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;
    private ArrowPool arrowPool;

    private Vector3 startPos; // �߻�Ǵ� ������ ��ġ
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
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= range)
        {
            Debug.Log("�ִ� �����Ÿ�");
            arrowPool.ReturnArrow(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "�� �浹");
        arrowPool.ReturnArrow(gameObject);
    }

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }
}
