using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatGate : MonoBehaviour
{
    private GateSpawner spawner;
    private GatePool gatePool;
    public StatType statType;
    private bool isTriggered = false;
    private float value = 1;

    private void Start()
    {
        gatePool = FindObjectOfType<GatePool>();
        if (gatePool == null)
            Debug.LogError("StatGate에서 gatePool을 찾을 수 없습니다.");
    }

    public void GateInit(GateSpawner gateSpawner, StatType type, int tileIdx, int chapter)
    {
        spawner = gateSpawner;
        statType = type;
        value = CheckStatValue(type, tileIdx, chapter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || spawner.IsTrigger())
            return;

        Player player = other.GetComponent<Player>();

        if (player != null)
            player.increaseStat(statType, value);

        gatePool.ReturnGate(gameObject);
        spawner.SetTrigger(true);
    }

    public float getValue()
    {
        return value;
    }

    private float CheckStatValue(StatType type, int tileIdx, int chapter)
    {
        float res = 0;

        switch (type)
        {
            case StatType.HP:
                res = (Random.Range(10f, 30f) + tileIdx) * chapter;
                break;
            case StatType.MOVESPEED:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.ARROWATK:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.ARROWRATE:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.ARROWSPEED:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.ARROWRANGE:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.ARROWCNT:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.SWORDATK:
                int tempATK = Random.Range(1, 3);
                res = (float)tempATK / 2f;
                break;
            case StatType.SWORDRATE:
                int tempRate = Random.Range(1, 3);

                if (tempRate == 1)
                    res = 5f;
                else
                    res = 10f;
                break;
            case StatType.SWORDSPEED:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.SWORDRANGE:
                res = (float)Random.Range(1, 3);
                break;
            case StatType.SWORDCNT:
                res = (float)Random.Range(1, 3);
                break;
            default:
                break;
        }

        return res;
    }
}
