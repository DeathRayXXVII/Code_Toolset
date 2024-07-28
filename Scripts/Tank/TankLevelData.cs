using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TankLevelData")]
public class TankLevelData : ScriptableObject
{
    public int levelNumber;
    public GameObject levelPrefab;
    public List<GameObject> tankPrefabs;
    public List<Vector3> tankSpawnPoints;
    public List<Vector3> playersTankSpawnPoints;
}
