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

    public void GateInit(GateSpawner gateSpawner, StatType type)
    {
        spawner = gateSpawner;
        statType = type;
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
}
