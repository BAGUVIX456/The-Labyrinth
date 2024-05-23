// Places item and enemy prefabs/SO using ItemPlacementHelper

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabPlacer : CorridorFirstGenerator
{
    
    public GameObject enemy;
    public GameObject player;
    
    private void PlacePrefab(Vector2Int position)
    {
        Instantiate(enemy, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }

    protected override void GenerateFloor()
    {
        CorridorFirstGeneration();

        bool firstRoom = true;
        foreach (var room in roomsDictionary)
        {
            if (firstRoom)
            {
                player.transform.position = new Vector3(room.Key.x, room.Key.y, 0);
                firstRoom = false;
                continue;
            }
            
            ItemPlacementHelper iph = new ItemPlacementHelper(room.Value, room.Value);

            Vector2Int position = iph.GetItemPlacementPosition(PlacementType.OpenSpace, 5, new Vector2Int(1, 1), false);
            for(int i=0; i<Random.Range(1, 3); i++)
            {
                PlacePrefab(position);
                position = iph.GetItemPlacementPosition(PlacementType.OpenSpace, 5, new Vector2Int(1, 1), false);
            }
        }
        
    }
}
