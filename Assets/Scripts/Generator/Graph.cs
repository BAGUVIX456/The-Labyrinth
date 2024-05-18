// Finds the valid neighbours in the rendered dungeon
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
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
    
    private List<Vector2Int> graph;
    
    public Graph(IEnumerable<Vector2Int> vertices)
    {
        graph = new List<Vector2Int>(vertices);
    }
    
    public List<Vector2Int> GetNeighbours4Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, cardinalDirectionsList);
    }
    
    public List<Vector2Int> GetNeighbours8Directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, eightDirectionsList);
    }
    
    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        foreach (var neighbourDirection in  neighboursOffsetList)
        {
            Vector2Int potentialNeighbour = startPosition + neighbourDirection;
            if (graph.Contains(potentialNeighbour))
                neighbours.Add(potentialNeighbour);
        }

        return neighbours;
    }
}
