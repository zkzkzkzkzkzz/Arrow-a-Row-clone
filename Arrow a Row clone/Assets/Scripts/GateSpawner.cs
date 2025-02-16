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

    private void Awake()
    {
        gatePool = FindObjectOfType<GatePool>();
        if (gatePool == null)
            Debug.LogError("gateSpawner에서 gatePool을 찾을 수 없습니다.");
    }

    public void SpawnGate()
    {
        ClearGates();

        StatType leftStat = GetRandomStat();
        StatType rightStat = GetRandomStat();

        leftGate = CreateGate(leftSpawn.position, leftStat);
        rightGate = CreateGate(rightSpawn.position, rightStat);
    }

    private GameObject CreateGate(Vector3 pos, StatType statType)
    {
        GameObject gate = gatePool.GetGate();
        gate .transform.position = leftSpawn.position;
        gate .GetComponent<StatGate>().statType = statType;
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

    /// <summary>
    /// 관문 선택지용 함수
    /// </summary>
    /// <returns></returns>
    private StatType GetRandomStat()
    {
        StatType[] statTypes = (StatType[])System.Enum.GetValues(typeof(StatType));
        return statTypes[Random.Range(0, statTypes.Length)];
    }
}
