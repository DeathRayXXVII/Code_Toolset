using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public int level;
    public int score;
    public int lives;
    public Brick[] bricks;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnlevelLoaded;
    }

    

    void Start()
    {
        NewGame();
    }


    void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        
        loadLevel(1);
    }
    
    public void loadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene(level);
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
