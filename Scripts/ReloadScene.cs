using UnityEngine;
using UnityEngine.SceneManagement;

    public class ReloadScene : MonoBehaviour
    {
        public void Reload()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
            string reload = GameState.Instance.CurrentAction;
        }
    }

