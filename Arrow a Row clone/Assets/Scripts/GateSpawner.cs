using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Debug.LogError("gateSpawner에서 gatePool을 찾을 수 없습니다.");
    }

    public void SpawnGate()
    {
        ClearGates();
        isTrigger = false;

        StatType leftStat = GetRandomGateStat();
        StatType rightStat = GetRandomGateStat();

        leftGate = CreateGate(leftSpawn.position, leftStat);
        rightGate = CreateGate(rightSpawn.position, rightStat);
    }

    private GameObject CreateGate(Vector3 pos, StatType statType)
    {
        GameObject gate = gatePool.GetGate();
        gate.transform.position = pos;

        int tileIdx = 0, chapter = 0;
        MapTileMgr tileMgr = FindAnyObjectByType<MapTileMgr>();
        if (tileMgr != null)
        {
            tileIdx = tileMgr.GetTileIdx();
            chapter = tileMgr.GetChapter();
        }

        gate.GetComponent<StatGate>().GateInit(this, statType, tileIdx, chapter);
        gate.transform.SetParent(transform);
        float value = gate.GetComponent<StatGate>().getValue();

        if (statType == StatType.SWORDATK)
            gate.GetComponentInChildren<TextMeshPro>().text = statType.GetStatName() + "\n+" + value.ToString("F1");
        else if (statType == StatType.SWORDRATE)
            gate.GetComponentInChildren<TextMeshPro>().text = statType.GetStatName() + "\n-" + ((int)value).ToString() + "%";
        else
            gate.GetComponentInChildren<TextMeshPro>().text = statType.GetStatName() + "\n+" + ((int)value).ToString();

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
    /// 관문 선택지용 함수
    /// </summary>
    /// <returns></returns>
    private StatType GetRandomGateStat()
    {
        StatType[] validStats = System.Enum.GetValues(typeof(StatType)).Cast<StatType>()
                                .Where(stat => stat.CanBeAppliedInGate())
                                .ToArray();

        return validStats[Random.Range(0, validStats.Length)];
    }
}
