using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Player;

public class StatGate : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.GetComponent<Player>();

        if (player != null)
            player.increaseStat(statType, value);

        gatePool.ReturnGate(gameObject);
    }

    public float getValue()
    {
        return value;
    }
}
