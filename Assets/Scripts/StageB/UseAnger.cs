using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseAnger : MonoBehaviour
{
    public GameObject anger;
    public Tilemap tilemap;
    public List<Vector2Int> tilemapCellList = new List<Vector2Int>();

    private void OnEnable()
    {
        SetAngerPos();
    }

    private void SetAngerPos()
    {
        int randNum = Random.Range(6, 15);
        for (int i = 0; i < randNum; i++)
        {

            GameObject generatedAnger = Instantiate(anger);

            Vector2Int[] tileCoordinates = GetTilemapCells(tilemap);
            int randomIndex = Random.Range(0, tileCoordinates.Length);
            Vector2 randomTileCoordinate = tileCoordinates[randomIndex];

            generatedAnger.transform.position = randomTileCoordinate;
        }
    }

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
