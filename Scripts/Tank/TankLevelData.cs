using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TankLevelData")]
public class TankLevelData : ScriptableObject
{
    public int levelNumber;
    public GameObject LevelPrefab;
    public List<GameObject> TankPrefabs;
    public List<Vector3> TankSpawnPoints;
    public List<Vector3> PlayersTankSpawnPoints;
}
