using UnityEngine;

[CreateAssetMenu(fileName = "Mobs_", menuName = "PrefabPlacer/Mob")]
public class EnemySO : ScriptableObject
{
    public GameObject sprite;
    public int max = 0;
    public int min = 0;
}