using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] Grid grid;
    [SerializeField] GameObject startingTile;
    [SerializeField] List<GameObject> tilePrefabs;

    private void Start()
    {
        GenerateTile(new Vector3Int(0, 0, 0), startingTile);
    }

    private void GenerateTile(Vector3Int gridLocation, GameObject tileToLoad)
    {
        var worldPosition = grid.GetCellCenterWorld(gridLocation);
        Instantiate(tileToLoad, worldPosition, Quaternion.identity);
    }

    public void GenerateNewRandomTile(Vector3Int gridLocation)
    {
        var tileToLoad = tilePrefabs[Random.Range(0, tilePrefabs.Count)];

        GenerateTile(gridLocation, tileToLoad);
    }


}
