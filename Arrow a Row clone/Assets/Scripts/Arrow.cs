using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Player player;
    private ObjPool objPool;

    private Vector3 startPos; // 발사되는 순간의 위치
    private float range;

    private bool isPenetration;
    private int hitCount;

    void Start()
    {
        objPool = FindObjectOfType<ObjPool>();
    }

    void OnEnable()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
            range = player.GetPlayerStats().ArrowRange;
        else
            Debug.LogError("플레이어를 찾을 수 없습니다.");

        if (player.GetPlayerItemStats().PenetrationLV > 0)
            isPenetration = true;

        hitCount = 0;
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
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("FinalBoss"))
        {
            if (!isPenetration)
            {
                if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
                    other.GetComponent<Monster>().TakeDamage(Mathf.CeilToInt(player.GetFinalArrowATK()));
                else
                    other.GetComponent<FinalBoss>().TakeDamage(Mathf.CeilToInt(player.GetFinalArrowATK()));

                if (player.HasLifeSteal())
                {
                    int hp = Mathf.RoundToInt(player.GetFinalArrowATK() * (player.GetPlayerItemStats().LifeSteal / 100f));
                    player.IncreaseStat(StatType.HP, hp);
                }

                objPool.ReturnArrow(gameObject);
            }
            else
            {
                int dmg = 0;

                if (hitCount == 0)
                    dmg = Mathf.CeilToInt(player.GetFinalArrowATK());
                else if (hitCount == 1)
                    dmg = Mathf.RoundToInt(player.GetFinalArrowATK() * (player.GetPlayerItemStats().PenetrationDamage / 100f));

                if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
                    other.GetComponent<Monster>().TakeDamage(dmg);
                else
                    other.GetComponent<FinalBoss>().TakeDamage(dmg);

                if (player.HasLifeSteal())
                {
                    int hp = Mathf.RoundToInt(dmg * (player.GetPlayerItemStats().LifeSteal / 100f));
                    player.IncreaseStat(StatType.HP, hp);
                }

                ++hitCount;

                if (hitCount >= 2)
                    objPool.ReturnArrow(gameObject);
            }
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
