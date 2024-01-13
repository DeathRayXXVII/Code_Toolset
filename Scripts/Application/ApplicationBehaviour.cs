using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class ApplicationBehaviour : MonoBehaviour
    {
        public int sceneToLoad;

        public void StartGame()
        {
            SceneManager.LoadScene(sceneToLoad);
            Time.timeScale = 1f;
        }
    
    }
}
