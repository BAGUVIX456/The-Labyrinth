using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfiniteWorldGenerator : MonoBehaviour
{
    public NavMeshSurface surface;
    public RandomWalkSO roomParameters;
    public TilemapVisualizer tilemapVisualizer;

    public int corridorLength = 20;
    public int numberOfRooms = 10;

    private RoomGenerator roomGenerator;
    private CorridorGenerator corridorGenerator;
    
    private HashSet<Vector2Int> floor = new();
    private List<HashSet<Vector2Int>> roomTiles = new();

    private void Start()
    {
        roomGenerator = new RoomGenerator(roomParameters, tilemapVisualizer);
        corridorGenerator = new CorridorGenerator();
        GenerateRoomCorridorPair(Vector2Int.zero);
        
        for(int i=0; i<numberOfRooms-1; i++)
            GenerateRoomCorridorPair(corridorGenerator.corridorEnd);
        
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
        
        surface.BuildNavMesh();
    }

    private void GenerateRoomCorridorPair(Vector2Int startPosition)
    {
        HashSet<Vector2Int> room = roomGenerator.GenerateRoom(startPosition);
        roomTiles.Add(room);
        
        HashSet<Vector2Int> corridor = corridorGenerator.GenerateCorridor(room.ElementAt(Random.Range(0, room.Count)), corridorLength); 
        
        room.UnionWith(corridor);
        floor.UnionWith(room);
    }
}