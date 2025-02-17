using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    private GatePool gatePool;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    private GameObject leftGate, rightGate;
    private bool isTrigger = false;

    private void Awake()
    {
        gatePool = FindObjectOfType<GatePool>();
        if (gatePool == null)
            Debug.LogError("gateSpawner���� gatePool�� ã�� �� �����ϴ�.");
    }

    public void SpawnGate()
    {
        ClearGates();
        isTrigger = false;

        StatType leftStat = GetRandomStat();
        StatType rightStat = GetRandomStat();

        leftGate = CreateGate(leftSpawn.position, leftStat);
        rightGate = CreateGate(rightSpawn.position, rightStat);
    }

    private GameObject CreateGate(Vector3 pos, StatType statType)
    {
        GameObject gate = gatePool.GetGate();
        gate.transform.position = pos;
        gate.GetComponent<StatGate>().GateInit(this, statType);
        gate.transform.SetParent(transform);
        float value = gate.GetComponent<StatGate>().getValue();
        gate.GetComponentInChildren<TextMeshPro>().text = statType.ToString() + " +" + ((int)value).ToString();

        return gate;
    }

    private void ClearGates()
    {
        List<Transform> gates = new List<Transform>();

        foreach (Transform t in transform)
        {
            if (t.CompareTag("Gate") && t.gameObject.activeSelf)
                gates.Add(t);
        }

        for (int i = 0; i < gates.Count; ++i)
        {
            gatePool.ReturnGate(gates[i].gameObject);
        }
    }

    public bool IsTrigger()
    {
        return isTrigger;
    }

    public void SetTrigger(bool b)
    {
        isTrigger = b;
    }

    /// <summary>
    /// ���� �������� �Լ�
    /// </summary>
    /// <returns></returns>
    private StatType GetRandomStat()
    {
        StatType[] statTypes = (StatType[])System.Enum.GetValues(typeof(StatType));
        return statTypes[Random.Range(0, statTypes.Length)];
    }
}
