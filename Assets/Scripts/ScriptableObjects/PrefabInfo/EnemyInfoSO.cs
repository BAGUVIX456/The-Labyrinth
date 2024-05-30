using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobData_", menuName = "PrefabPlacer/MobData")]
public class EnemyInfoSO : ScriptableObject
{
    public List<EnemySO> Mob = new List<EnemySO>();
}
