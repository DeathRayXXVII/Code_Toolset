using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] private LevelManager currentLevelData;
    [SerializeField] private GameObject levelPosition;
    [SerializeField] private List<LevelManager> levelDataList;
    [SerializeField] private List<GameObject> playersTankPrefab;
    //[SerializeField] private List<GameObject> enemyTankPrefabs;
    [SerializeField] private List<GameObject> currentEnemyTankPrefab;
    [SerializeField] private List<GameObject> playersSpawnPoint;
    private UnityEvent onLevelComplete;
    [SerializeField] private bool isRestarting;
    
    public UnityEvent winEvent;
    
    private void Awake()
    {
        if (currentLevelData == null)
        {
            Debug.LogError("Level data is not set in the TankGameManager");
            return;
        }

        if (!isRestarting)
        {
            
            LevelGenerator();
        }

    }

    private void Update()
    {
        // if(currentEnemyTankPrefab.Count == 0)
        // {
        //     onLevelComplete.Invoke();
        // }
    }

    private void LevelGenerator()
    {
        currentLevelData.transform.position = Vector3.zero;
        
        foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
        {
            currentEnemyTankPrefab.Add(obj);
        }
        
        playersSpawnPoint.Clear();
        foreach (GameObject obj in currentLevelData.playersSpawnPoints)
        {
            playersSpawnPoint.Add(obj);
        }
        
        for (int i = 0; i < playersTankPrefab.Count; i++)
        {
            playersTankPrefab[i].transform.position = playersSpawnPoint[i].transform.position;
        }
        currentLevelData.LevelActive();
    }
    
    private void LevelReset()
    {
        foreach (GameObject obj in currentEnemyTankPrefab)
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
        currentLevelData.ExitLevel();
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
            currentEnemyTankPrefab.Clear();
            Debug.Log("You win!");
        }
    }
    
    public bool ClearedLevel()
    {
        bool cleared = true;
        foreach (GameObject obj in currentEnemyTankPrefab)
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
