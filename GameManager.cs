using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int score;
    public int lives;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
}
