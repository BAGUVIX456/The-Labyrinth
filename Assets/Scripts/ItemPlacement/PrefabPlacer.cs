using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{
    public static void PlaceMobs(EnemyInfoSO enemyInfo, HashSet<Vector2Int> room, int numberOfMobs)
    {
        for (int i = 0; i < numberOfMobs; i++)
        {
            Vector2Int spawnPoint = room.ElementAt(Random.Range(0, room.Count));
            Vector3 spawnPosition = new Vector3(spawnPoint.x, spawnPoint.y, 0.1f);
            Instantiate(enemyInfo.Mob[0].sprite, spawnPosition, Quaternion.identity);
        }
    }
}
