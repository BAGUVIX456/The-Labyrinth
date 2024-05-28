using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public static class WallGenerator
{
    private static HashSet<Vector2Int> sideWalls = new();
    
    // Returns position of removed wall outlining a room
    public static Vector2Int CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        sideWalls.Clear();
        
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D_.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D_.diagonalDirectionsList);

        var toBeRemoved = sideWalls.ElementAt(Random.Range(0, sideWalls.Count));
        //basicWallPositions.Remove(toBeRemoved);
        
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);

        return toBeRemoved;
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions,
        HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D_.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }
    
    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighbooursBinaryType = "";
            foreach (var direction in Direction2D_.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighbooursBinaryType += "1";
                }
                else
                {
                    neighbooursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighbooursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);

                    if (directionList == Direction2D_.cardinalDirectionsList &&
                        (direction == Vector2Int.left || direction == Vector2Int.right))
                        sideWalls.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
