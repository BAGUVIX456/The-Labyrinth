using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;

    protected override void GenerateRoom()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = RandomWalk.Walk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
