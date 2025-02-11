using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        // 초기 타일 생성
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

        if (activeTiles.Count == 0 || isNeedNewTile())
        {
            SpawnTile();
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
    }

    /// <summary>
    /// 타일 재활용 체크 함수
    /// 타일의 z값이 recycleZ보다 작아지면 재활용 풀에 넣음
    /// </summary>
    private void CheckTiles()
    {
        while (activeTiles.Count > 0)
        {
            GameObject tile = activeTiles[0];
            if (tile.transform.position.z <= recycleZ)
            {
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
        MapTile mapTile = tile.GetComponent<MapTile>();
        if (mapTile != null)
            mapTile.SpawnMonster(tileIdx);

        ++tileIdx;
        if (tileIdx >= 6)
        {
            tileIdx = 0;
            ++chapter;
        }
    }
}
