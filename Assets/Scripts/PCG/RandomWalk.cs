// Contains the actual Random Walk algorithm

using System.Collections.Generic;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> Walk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); 
        path.Add(startPosition);

        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }

    public static List<Vector2Int> CorridorWalk(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
    };
    
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1, 1), //UP-RIGHT
        new Vector2Int(1, -1), //RIGHT-DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 1), //UP-RIGHT
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(1, -1), //RIGHT-DOWN
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, -1), //DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static Vector2Int GetRandomDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
