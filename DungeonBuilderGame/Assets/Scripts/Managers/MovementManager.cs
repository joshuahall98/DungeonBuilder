using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager movementManagerInstance;

    [SerializeField] TileGenerator tileGenerator;

    private void Awake()
    {
        movementManagerInstance = this;
    }

    public Vector3Int MoveNorth(Vector3Int currentPosition)
    {
        var newPosition = new Vector3Int(currentPosition.x, currentPosition.y, currentPosition.z + 1);
        tileGenerator.GenerateNewRandomTile(newPosition);
        return newPosition;
    }

    public Vector3Int MoveEast(Vector3Int currentPosition)
    {
        var newPosition = new Vector3Int(currentPosition.x + 1, currentPosition.y, currentPosition.z);
        tileGenerator.GenerateNewRandomTile(newPosition);
        return newPosition;
    }

    public Vector3Int MoveWest(Vector3Int currentPosition)
    {
        var newPosition = new Vector3Int(currentPosition.x - 1, currentPosition.y, currentPosition.z);
        tileGenerator.GenerateNewRandomTile(newPosition);
        return newPosition;
    }

    public Vector3Int MoveSouth(Vector3Int currentPosition)
    {
        var newPosition = new Vector3Int(currentPosition.x, currentPosition.y, currentPosition.z - 1);
        tileGenerator.GenerateNewRandomTile(newPosition);
        return newPosition;
    }
}
