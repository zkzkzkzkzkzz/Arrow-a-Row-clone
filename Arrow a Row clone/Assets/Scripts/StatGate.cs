using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatGate : MonoBehaviour
{
    private GateSpawner spawner;
    private GatePool gatePool;
    public StatType statType;
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
                res = (Random.Range(10, 31) + tileIdx) * chapter * 2;
                break;
            case StatType.MOVESPEED:
            case StatType.ARROWATK:
            case StatType.ARROWRATE:
            case StatType.ARROWSPEED:
            case StatType.ARROWRANGE:
            case StatType.ARROWCNT:
            case StatType.SWORDSPEED:
            case StatType.SWORDRANGE:
            case StatType.SWORDCNT:
                res = (float)WeightedRandom(1, 2, 0.9f);
                break;
            case StatType.SWORDATK:
                res = (float)WeightedRandom(1, 2, 0.9f) / 2f; 
                break;
            case StatType.SWORDRATE:
                res = (float)WeightedRandom(1, 2, 0.9f) * 5f;
                break;
            default:
                break;
        }

        return res;
    }

    private int WeightedRandom(int v1, int v2, float fvalue)
    {
        return Random.value < fvalue ? v1 : v2;
    }
}
