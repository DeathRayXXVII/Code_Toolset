using UnityEngine;

public class OnScreenMouse : MonoBehaviour
{
    public bool lockCursor = true;
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.visible = false;
        }
    }
}
