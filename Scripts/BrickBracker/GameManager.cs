using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public partial class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public int level;
    public IntData life;
    public Brick bricks;
    public List<GameObject> spawnedObjects = new List<GameObject>();
    private BrickSpawn brickSpawn;
    public UnityEvent newGameEvent, winEvent;
    //public Vector3DataList brickPosition;
    //public int instancerDataListObj;
    public UnityEvent noLifeEvent;
    //public float spawnWeight = 1f;
    private BrickData brickData;
    private BrickSpawn bS;
    
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnlevelLoaded;
    }
    
    public void NewGame()
    {
        newGameEvent.Invoke();
        //SpawnBricks();
        ResetLevel();
        
        foreach (GameObject obj in spawnedObjects)
        {
            Brick brick = obj.GetComponent<Brick>();
            if (!brick.gameObject.activeInHierarchy)
            {
                brick.ResetBrick();
            }
        }
    }
    
    public void loadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene(level);
        bS.SpawnBricks();
    }
    private void OnlevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
    }
    public void ResetLevel()
    {
        ball.ResetBall();
        paddle.ResetPaddle();

         
    }
    public void LoseLife()
    {
        if (life.value <= 0)
        {
            noLifeEvent.Invoke();
        }
        else
        {
            ResetLevel();
        }
    }

    public void Hit()
    {
        if (ClearedLevel())
        {
            winEvent.Invoke();
            Debug.Log("You win!");
        }
    }
    private bool ClearedLevel()
    {
        bool cleared = true;
        foreach (GameObject obj in spawnedObjects)
        {
            Brick brick = obj.GetComponent<Brick>();
            if (brick.gameObject.activeInHierarchy && !brick.unbreakable)
            {
                cleared = false;
                break;
            }
        }
        return cleared;
    }
    
    /*void SpawnBricks()
    {
        if (bricks == null || bricks.Length == 0)
        {
            Debug.LogError("No brick prefabs assigned. Please assign at least one prefab.");
            return;
        }

        if (brickPosition == null || brickPosition.positionList.Count == 0)
        {
            Debug.LogError("No spawn position found in the Vector3List ScriptableObject. Please assign a valid Vector3List asset.");
            return;
        }

        for (int i = 0; i < bricks.Length; i++)
        {
            // Randomly select a prefab based on spawn weights
            GameObject selectedPrefab = ChooseRandomPrefab();

            // Randomly select a spawn position
            //Vector3t randomPosition = brickPosition.positionList[Random.Range(0, brickPosition.positionList.Count)];

            // Spawn the selected prefab at the random position
            //Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
        }
    }
    GameObject ChooseRandomPrefab()
    {
        float totalWeight = 0f;

        foreach (var prefabData in bricks)
        {
            totalWeight += prefabData.spawnWeight;
        }

        float randomValue = Random.Range(0f, totalWeight);

        foreach (var prefabData in bricks)
        {
            if (randomValue <= prefabData.spawnWeight)
            {
                return prefabData.gameObject;
            }

            randomValue -= prefabData.spawnWeight;
        }

        return bricks[bricks.Length - 1].gameObject;
    }*/
    
}
