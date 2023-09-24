using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public int level;
    public intData life;
    public Brick[] bricks;
    public UnityEvent newGameEvent;
    public Vector3DataList brickPosition;
    public int instancerDataListObj;
    public UnityEvent noLifeEvent;
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnlevelLoaded;
    }
    
    void Start()
    {
        NewGame();
    }
    

    void NewGame()
    {
        newGameEvent.Invoke();
        loadLevel(1);
    }
    
    public void loadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene(level);
        //instancerDataListObj = Random.Range(bricks.Length, bricks.Length);
    }
    private void OnlevelLoaded(Scene scene, LoadSceneMode mode)
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
    }
    private void ResetLevel()
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

    public void Hit(Brick brick)
    {
        if (ClearedLevel())
        {
            loadLevel(level);
        }
    }
    private bool ClearedLevel()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }
}
