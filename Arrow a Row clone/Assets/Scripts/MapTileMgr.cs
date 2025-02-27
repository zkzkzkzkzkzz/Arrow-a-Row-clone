using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
#if UNITY_EDITOR
using Unity.VisualScripting.ReorderableList;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTileMgr : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform lastTileCheck;
    [SerializeField] private float tileSpeed = 3f;
    [SerializeField] private float tileLength = 25f;
    [SerializeField] private float recycleZ = -21f;

    [SerializeField] private List<GameObject> mapTileTemplates;

    private List<GameObject> activeTiles = new List<GameObject>();
    private Queue<GameObject> recyclePool = new Queue<GameObject>();

    private int tileIdx = 0;
    private int chapter = 1;

    private int curTileIdx = 0;
    private int curChapter = 1;
    private int recycleTileCnt = 0;

    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        // �ʱ� Ÿ�� ����
        for (int i = 0; i < 3; ++i)
        {
            SpawnTile();
        }
    }

    private void Update()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(0, 0, -tileSpeed * Time.deltaTime);
        }

        CheckTiles();
        UpdatePlayerTileIdx();

        if (IsPlayerOnFinalBossTile())
        {
            SetTileSpeed(0f);
            if (cameraController != null)
                cameraController.SetTPSMode(true);
        }
        else
        {
            SetTileSpeed(tileSpeed);
            if (cameraController != null)
                cameraController.SetTPSMode(false);
        }

        if (activeTiles.Count == 0 || isNeedNewTile())
        {            
            SpawnTile();
        }
    }

    /// <summary>
    /// �÷��̾ ���� �� ��° é���� �� �� �ε��� Ÿ���� ��� �ִ��� ���
    /// </summary>
    private void UpdatePlayerTileIdx()
    {
        curTileIdx = recycleTileCnt % 6;
        if (recycleTileCnt >= 6)
        {
            ++curChapter;
            ++tileSpeed;
            recycleTileCnt = 0;
        }
    }

    private void SpawnTile()
    {
        GameObject tile = null;

        if (recyclePool.Count > 0)
        {
            tile = recyclePool.Dequeue();
            tile.SetActive(true);
        }
        else
        {
            int idx = Random.Range(0, mapTileTemplates.Count);
            tile = Instantiate(mapTileTemplates[idx]);
        }

        Vector3 spawnPos = spawnPoint.position;
        if (activeTiles.Count > 0)
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];
            spawnPos = lastTile.transform.position + new Vector3(0, 0, tileLength);
        }
        tile.transform.position = spawnPos;

        activeTiles.Add(tile);

        SpawnMonsterOnTile(tile);

        tile.GetComponent<GateSpawner>().SpawnGate();
    }

    /// <summary>
    /// Ÿ�� ��Ȱ�� üũ �Լ�
    /// Ÿ���� z���� recycleZ���� �۾����� ��Ȱ�� Ǯ�� ����
    /// </summary>
    private void CheckTiles()
    {
        while (activeTiles.Count > 0)
        {
            GameObject tile = activeTiles[0];
            if (tile.transform.position.z <= recycleZ)
            {
                ++recycleTileCnt;
                tile.SetActive(false);
                activeTiles.RemoveAt(0);
                recyclePool.Enqueue(tile);
            }
            else
                break;
        }
    }

    private bool isNeedNewTile()
    {
        GameObject lastTile = activeTiles[activeTiles.Count - 1];
        return lastTile.transform.position.z < lastTileCheck.position.z;
    }

    private void SpawnMonsterOnTile(GameObject tile)
    {
        if (tileIdx == 0 && chapter == 1)
        {
            ++tileIdx;
            return;
        }

        if (IsFinalBossTile())
        {
            MapTile mapTile = tile.GetComponent<MapTile>();
            if (mapTile != null)
                mapTile.SpawnFinalBoss(tileIdx, chapter);
        }
        else
        {
            MapTile mapTile = tile.GetComponent<MapTile>();
            if (mapTile != null)
                mapTile.SpawnMonster(tileIdx, chapter);
        }

        ++tileIdx;
        if (tileIdx >= 6)
        {
            tileIdx = 0;
            ++chapter;
        }
    }

    public int GetTileIdx()
    {
        return tileIdx;
    }

    public int GetChapter()
    {
        return chapter;
    }


    /// <summary>
    /// �÷��̾ ���� ��� �ִ� Ÿ���� �� é���� �� �� �ε������� ��ȯ
    /// </summary>
    /// <returns></returns>
    public int GetPlayerTileIdx()
    {
        return curTileIdx;
    }
    public int getPlayerChapter()
    {
        return curChapter;
    }

    public float GetTileSpeed()
    {
        return tileSpeed;
    }

    public void SetTileSpeed(float speed)
    {
        tileSpeed = speed;
    }

    /// <summary>
    /// ������ �� Ÿ���� ���� ���� Ÿ������ ��ȯ
    /// </summary>
    public bool IsFinalBossTile()
    {
        return (tileIdx == 5 && chapter == 13) ? true : false;
    }

    /// <summary>
    /// �÷��̾ ���� ���� ���� Ÿ�Ͽ� �����ߴ��� ��ȯ
    /// </summary>
    private bool IsPlayerOnFinalBossTile()
    {
        return (curTileIdx == 5 && curChapter == 13) ? true : false;
    }
}
