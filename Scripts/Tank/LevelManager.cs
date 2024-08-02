using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNumber;
    public Vector3 levelPosition;
    public List<GameObject> enemyTankPrefabs;
    public List<GameObject> enemySpawnPoints;
    public List<GameObject> playersSpawnPoints;

    private void Start()
    {
        levelPosition = transform.position;
    }

    public void LevelActive()
    {
        gameObject.SetActive(true);
    }
    
    public void ExitLevel()
    {
        transform.position = levelPosition;
        gameObject.SetActive(false);
    }
}