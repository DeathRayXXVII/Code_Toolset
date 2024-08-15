using System;
using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] private LevelManager currentLevelData;
    [SerializeField] private EnemyTank enemyTank;
    [SerializeField] private TankController playerTank;
    [SerializeField] private GameObject levelPosition;
    [SerializeField] private IntData enemyTankNum;
    [SerializeField] private List<LevelManager> levelDataList;
    [SerializeField] private List<GameObject> playersTankPrefab;
    [SerializeField] private List<GameObject> playersSpawnPoint;
    public UnityEvent onLevelComplete, onGameComplete, onLevelFail;
    [SerializeField] private bool isRestarting;
    private bool cleared;
    private int nextLevel;
    //public bool playerPosisionUpdate;
    
    private void Awake()
    {
        enemyTank = GetComponent<EnemyTank>();
        playerTank = GetComponent<TankController>();
    }
    private void Start()
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

    private void LevelGenerator()
    {
        currentLevelData.transform.position = Vector3.zero;
        playersSpawnPoint.Clear();
        foreach (GameObject obj in currentLevelData.playersSpawnPoints)
        {
            playersSpawnPoint.Add(obj);
        }
        for (int i = 0; i < playersTankPrefab.Count; i++)
        {
            playersTankPrefab[i].transform.position = playersSpawnPoint[i].transform.position;
            playerTank = playersTankPrefab[i].GetComponent<TankController>();
            playerTank.SetSpawnPoint(playersSpawnPoint[i].transform.position);
            Debug.Log("Player " + i + " spawned at " + playersSpawnPoint[i].transform.position);
        }
        currentLevelData.LevelActive();
    }
    
    public void LevelReset()
    {
        foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            if (!enemyTank.gameObject.activeInHierarchy)
            {
                enemyTank.gameObject.SetActive(true);
            }
            enemyTank.ResetTank();
        }
        foreach (GameObject obj in playersTankPrefab)
        {
            TankController playerTank = obj.GetComponent<TankController>();
            playerTank.gameObject.SetActive(true);
        }
    }

    private void LoadLevel(int levelNumber)
    {
        if (levelNumber < 0 || levelNumber >= levelDataList.Count)
        {
            Debug.Log("Invalid level number");
            return;
        }
        currentLevelData = levelDataList[levelNumber];
        LevelGenerator();
    }
    
    public void LoadNextLevel()
    {
        currentLevelData.ExitLevel();
        
        if (nextLevel >= levelDataList.Count)
        {
            Debug.Log("No more levels to load");
            onGameComplete.Invoke();
            return;
        }
        LoadLevel(nextLevel);
    }
    
    public void LoadPreviousLevel()
    {
        int previousLevel = currentLevelData.levelNumber - 1;
        if (previousLevel < 0)
        {
            Debug.Log("No previous levels to load");
            return;
        }
        LoadLevel(previousLevel);
    }
    
    public void LevelFail()
    {
        foreach (var obj in playersTankPrefab)
        {
            if (!obj.activeInHierarchy)
            {
                //enemyTank.canMove = false;
                //isRestarting = true;
                onLevelFail.Invoke();
                //LevelReset();
                //LevelGenerator();
            }
        }
    }
    
    public void UpdateLevelInfo()
    {
        Debug.Log("Level " + currentLevelData.levelNumber);
        nextLevel = currentLevelData.levelNumber + 1;
        enemyTankNum.value = currentLevelData.enemyTankPrefabs.Count;
    }
    
    /*private void UpdatePlayerPosition()
    {
        if (playerPosisionUpdate)
        {
            Debug.Log("Player position update");
            for (int i = 0; i < playersTankPrefab.Count; i++)
            {
                playersTankPrefab[i].transform.position = playersSpawnPoint[i].transform.position;
                playerTank = playersTankPrefab[i].GetComponent<TankController>();
                playerTank.SetSpawnPoint(playersSpawnPoint[i].transform.position);
                Debug.Log("Player " + i + " spawned at " + playersSpawnPoint[i].transform.position);
            }
            playerPosisionUpdate = false;
        }
    }*/
    
    public void Hit()
    {
        enemyTankNum.value--;
        if (ClearedLevel())
        {
            foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
            {
                EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
                enemyTank.ResetBullet();
            }
            foreach (GameObject obj in playersTankPrefab)
            {
                TankShooting playerTank = obj.GetComponent<TankShooting>();
                playerTank.ResetBullets();
            }
            cleared = false;
            isRestarting = false;
            onLevelComplete.Invoke();
            Debug.Log("You win!");
        }
    }

    private bool ClearedLevel()
    {
        cleared = true;
        foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            if (enemyTank.gameObject.activeInHierarchy)
            {
                cleared = false;
                break;
            }
        }
        return cleared;
    }
    
    public void EnemyPause()
    {
        foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            enemyTank.canMove = false;
        }
    }
    
    public void EnemyResume()
    {
        foreach (GameObject obj in currentLevelData.enemyTankPrefabs)
        {
            EnemyTank enemyTank = obj.GetComponent<EnemyTank>();
            enemyTank.canMove = true;
        }
    }
}
