using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWorldGenerator : MonoBehaviour
{
    public RandomWalkSO roomParameters;
    public TilemapVisualizer tilemapVisualizer;

    private RoomGenerator_ roomGenerator;

    private void Start()
    {
        roomGenerator = new RoomGenerator_(roomParameters, tilemapVisualizer);
        roomGenerator.GenerateRoom(Vector2Int.zero);
    }

    private void Update()
    {
        
    }
}
