using System.Collections.Generic;
using UnityEngine;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] private TankLevelData levelData;
    [SerializeField] private List<GameObject> playersTankPrefab;
    [SerializeField] private List<Vector3> playersTankSpawnPoint;
    
    private void Start()
    {
        if (levelData == null)
        {
            Debug.LogError("Level data is not set in the TankGameManager");
            return;
        }
        Instantiate(levelData.LevelPrefab);
        foreach (var playerTankPrefab in playersTankPrefab)
        {
            foreach (var playerTankSpawnPoint in playersTankSpawnPoint)
            {
                Instantiate(playerTankPrefab, playerTankSpawnPoint, Quaternion.identity);
            }
        }
        
        foreach (var tankPrefab in levelData.TankPrefabs)
        {
            //int randomIndex = Random.Range(0, levelData.TankSpawnPoints.Count);
            foreach (var tankSpawnPoint in levelData.TankSpawnPoints)
            {
                Instantiate(tankPrefab, tankSpawnPoint, Quaternion.identity);
            }
            
            
        }
    }
}
