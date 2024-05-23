/*
 * TODO:
 *  1. Generate spawn room (done)
 *  2. Choose a wall to create a corridor in (bug: Outer wall needs to be removed, not the inner ones)
 */

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