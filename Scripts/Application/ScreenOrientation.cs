using UnityEngine;

namespace Scripts
{
    public class ScreenOrientation : MonoBehaviour
    {
        void Start()
        {
            //Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
            Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
        
        }

    }
}
