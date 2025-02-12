using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    private GatePool gatePool;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;

    private void Awake()
    {
        gatePool = FindObjectOfType<GatePool>();
        if (gatePool == null)
            Debug.LogError("gateSpawner���� gatePool�� ã�� �� �����ϴ�.");
    }

    public void SpawnGate()
    {
        ClearGates();

        StatType leftStat = GetRandomStat();
        StatType rightStat = GetRandomStat();

        GameObject leftGate = gatePool.GetGate();
        leftGate.transform.position = leftSpawn.position;
        leftGate.GetComponent<StatGate>().statType = leftStat;
        leftGate.transform.SetParent(transform);
        float lvalue = leftGate.GetComponent<StatGate>().getValue();
        leftGate.GetComponentInChildren<TextMeshPro>().text = leftStat.ToString() + " +" + ((int)lvalue).ToString();


        GameObject rightGate = gatePool.GetGate();
        rightGate.transform.position = rightSpawn.position;
        rightGate.GetComponent<StatGate>().statType = rightStat;
        rightGate.transform.SetParent(transform);
        float rvalue = rightGate.GetComponent<StatGate>().getValue();
        rightGate.GetComponentInChildren<TextMeshPro>().text = rightStat.ToString() + " +" + ((int)rvalue).ToString();
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
