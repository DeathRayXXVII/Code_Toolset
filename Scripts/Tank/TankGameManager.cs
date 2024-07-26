using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] private TankLevelData currentLevelData;
    [SerializeField] private List<TankLevelData> levelDataList;
    [SerializeField] private List<GameObject> playersTankPrefab;
    [SerializeField] private List<GameObject> enemyTankPrefab;
    [SerializeField] private List<Vector3> playersTankSpawnPoint;
    private UnityEvent onLevelComplete;
    [SerializeField] private bool isRestarting;
    
    public UnityEvent winEvent;
    
    private void Start()
    {
        if (currentLevelData == null)
        {
            Debug.LogError("Level data is not set in the TankGameManager");
            return;
        }

        if (!isRestarting)
        {
            enemyTankPrefab = currentLevelData.TankPrefabs;
            LevelGenerator();
        }

    }

    private void Update()
    {
        if(enemyTankPrefab.Count == 0)
        {
            onLevelComplete.Invoke();
        }
    }

    private void LevelGenerator()
    {
        Instantiate(currentLevelData.LevelPrefab);
        foreach (var playerTankPrefab in playersTankPrefab)
        {
            foreach (var playerTankSpawnPoint in playersTankSpawnPoint)
            {
                Instantiate(playerTankPrefab, playerTankSpawnPoint, Quaternion.identity);
            }
        }
        
        foreach (var tankPrefab in currentLevelData.TankPrefabs)
        {
            //int randomIndex = Random.Range(0, levelData.TankSpawnPoints.Count);
            foreach (var tankSpawnPoint in currentLevelData.TankSpawnPoints)
            {
                Instantiate(tankPrefab, tankSpawnPoint, Quaternion.identity);
            }
        }
    }
    
    private void LevelReset()
    {
        foreach (GameObject obj in enemyTankPrefab)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            if (!enemyTank.gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
            }
            
            
            enemyTank.ResetTank();
        }
    }

    private void LoadLevel(int levelNumber)
    {
        if (levelNumber < 0 || levelNumber >= levelDataList.Count)
        {
            Debug.LogError("Invalid level number");
            return;
        }
        currentLevelData = levelDataList[levelNumber];
        LevelGenerator();
    }
    
    public void LoadNextLevel()
    {
        int nextLevel = currentLevelData.levelNumber + 1;
        if (nextLevel >= levelDataList.Count)
        {
            Debug.LogError("No more levels to load");
            return;
        }
        LoadLevel(nextLevel);
    }
    
    public void LoadPreviousLevel()
    {
        int previousLevel = currentLevelData.levelNumber - 1;
        if (previousLevel < 0)
        {
            Debug.LogError("No previous levels to load");
            return;
        }
        LoadLevel(previousLevel);
    }
    
    public void Hit()
    {
        if (ClearedLevel())
        {
            isRestarting = false;
            winEvent.Invoke();
            enemyTankPrefab.Clear();
            Debug.Log("You win!");
        }
    }
    
    public bool ClearedLevel()
    {
        bool cleared = true;
        foreach (GameObject obj in enemyTankPrefab)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            if (enemyTank.gameObject.activeInHierarchy)
            {
                cleared = false;
                break;
            }
        }
        //enemyTankPrefab.Clear();
        //enemyTankSpawnPoint.Clear();
        //playersTankSpawnPoint.Clear();
        return cleared;
        
    }
}
