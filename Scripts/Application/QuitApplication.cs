using Scripts.Data;
using UnityEngine;
using Steamworks;

namespace Scripts
{
    public class QuitApplication : MonoBehaviour
    {
        public IntData scoreKey;
        public void QuitAplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit ();
            SteamClient.Shutdown();
            
#endif
        }

        public void SaveData()
        {
            //PlayerPrefs.SetInt(scoreKey);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
        
        }
    }
}
