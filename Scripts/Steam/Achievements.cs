using UnityEngine;

public class Achievements : MonoBehaviour
{
    public void IsAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        Debug.Log($"Achievement {id} status : " + ach.State);
        if (ach.State)
        {
            Debug.Log("Achievement already unlocked");
        }
        else
        {
            UnlockAchievement(id);
        }
    }
    
    public void UnlockAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();
        Debug.Log($"Achievement {id} unlocked");
    }
    
    public void ResetAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();
        Debug.Log($"Achievement {id} cleared");
    }
}
