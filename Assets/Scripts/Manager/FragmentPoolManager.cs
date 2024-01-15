using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FragmentPoolManager : MonoBehaviour
{
    public static FragmentPoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public GameObject[] fragmentPrefabs;
    public List<GameObject> createdFragments;
    public List<GameObject> usedFragments;
    public Tilemap tilemap;
    public List<Vector2Int> tilemapCellList = new List<Vector2Int>();

    private void Start()
    {
        CreatRandomFragments();
        ArrangeFragments();
    }

    private void Update()
    {
        GenerateInGame();
    }

    private void CreatRandomFragments()
    {
        for (int i = 0; i < 100;  i++)
        {
            int randomNum = Random.Range(0, 2);
            GameObject create = Instantiate(fragmentPrefabs[randomNum], transform);
            createdFragments.Add(create);
        }
    }

    private void ArrangeFragments()
    {
        for (int i = 0; i < createdFragments.Count; i++)
        {
            Transform arrangePos = createdFragments[i].transform;
            // TODO : 콜라이더를 제외한 맵 안에 fragments가 배치되도록 변경
            //arrangePos.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            Vector2Int[] tileCoordinates = GetTilemapCells(tilemap);
            int randomIndex = Random.Range(0, tileCoordinates.Length);
            Vector2 randomTileCoordinate = tileCoordinates[randomIndex];

            arrangePos.position = randomTileCoordinate;
        }
    }

    public void HideAteFragments(HappyDreamFragmentE fragment)
    {
        usedFragments.Add(fragment.gameObject);
        createdFragments.Remove(fragment.gameObject);
        fragment.gameObject.SetActive(false);
    }

    public void GenerateInGame()
    {
        if (createdFragments.Count < 10)
        {
            for (int i = 0; i < 5; i++)
            {
                int randomNum = Random.Range(0, 2);
                GameObject newFragments = Instantiate(fragmentPrefabs[randomNum], transform);
                createdFragments.Add(newFragments);
                // TODO : 콜라이더를 제외한 맵 안에 fragments가 배치되도록 변경
                //newFragments.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                Vector2Int[] tileCoordinates = GetTilemapCells(tilemap);
                int randomIndex = Random.Range(0, tileCoordinates.Length);
                Vector2 randomTileCoordinate = tileCoordinates[randomIndex];

                newFragments.transform.position = randomTileCoordinate;
            }
        }
    }

    //타일맵의 좌표 저장하기
    Vector2Int[] GetTilemapCells(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.x; x < bounds.x + bounds.size.x; x++)
        {
            for (int y = bounds.y; y < bounds.y + bounds.size.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (tilemap.HasTile(tilePosition))
                {
                    tilemapCellList.Add(new Vector2Int(x, y));
                }
            }
        }

        return tilemapCellList.ToArray();
    }
}
