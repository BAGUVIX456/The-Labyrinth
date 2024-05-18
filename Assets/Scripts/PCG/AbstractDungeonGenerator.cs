// Abstract Class that all other PCG classes derive from
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    // Generate dungeon, either corridor first or just simple random walk
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        GenerateRoom();
    }

    // Clears level
    public void ClearDungeon()
    {
        tilemapVisualizer.Clear();

        /*
        while (true)
        {
            GameObject e = GameObject.Find("Enemy(Clone)");
            
            if (e) 
                Destroy(e);
            else 
                break;
        }
        */
    }

    protected abstract void GenerateRoom();
}
