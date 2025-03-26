using System.Collections.Generic;
using Scripts.UnityActions;
using UnityEngine;

namespace Scripts.Achievements
{
    [CreateAssetMenu(fileName = "AchievementData", menuName = "Achievement Data")]
    public class AchievementData : ScriptableObject
    {
        [SerializeReference, SubclassSelector]
        public List<Achievements> achievements = new List<Achievements>();
    }

    [System.Serializable]
    public abstract class Achievements
    {
        public string idName;
        public string name;
        public string description;
        public bool isHidden;
        public bool isUnlocked;
        public Sprite lockedIcon;
        public Sprite unlockedIcon;
        public GameAction action;
        //Unity parallel task in manager, refrence level select
        //private void OnEnable() => action.RaiseEvent += CheckProgress;
        //private void OnDisable() => action.RaiseEvent -= CheckProgress;
        
        /*private void OnEnable()
        {
            if (action != null)
            {
                action.RaiseEvent += CheckProgress;
            }

            Debug.Log($"Action is null: {action == null}");
        }
        
        private void OnDisable()
        {
            if (action != null)
            {
                action.RaiseEvent -= CheckProgress;
            }
        }*/
        
        public abstract void CheckProgress(GameAction _);
    }
    
    [System.Serializable]
    public class Achievement : Achievements
    {
        protected Achievement(bool newIsUnlocked)
        {
            isUnlocked = newIsUnlocked;
        }
        public Achievement() { }

        public override void CheckProgress(GameAction _)
        {
            if (isUnlocked) return;
            isUnlocked = true;
            Debug.Log($"Achievement {idName} unlocked");
        }
    }

    [System.Serializable]
    public class ProgressiveAchievement : Achievements
    {
        protected ProgressiveAchievement(float newProgress, bool newIsUnlocked)
        {
            progress = newProgress;
            isUnlocked = newIsUnlocked;
        }
        public ProgressiveAchievement() { }

        public override void CheckProgress(GameAction _)
        {
            if (progress >= goal) return;
            progress++;
            if (!(progress >= goal)) return;
            isUnlocked = true;
            Debug.Log($"Achievement {idName} unlocked");
        }
        
        public float goal;
        public float progress;
        public int progressUpdate;
        public string progressSuffix;
        public float notify;
    }
    
    [System.Serializable]
    public struct CollectionAch
    {
        public ID id;
        public bool isCollected;
    }
    [System.Serializable]
    public class CollectiveAchievement : Achievements
    {
        protected CollectiveAchievement(bool newIsUnlocked)
        {
            isUnlocked = newIsUnlocked;
        }
        public CollectiveAchievement() { }

        public override void CheckProgress(GameAction _)
        {
            if (collection.TrueForAll(x => x.isCollected)) return;
            collection.ForEach(x => x.isCollected = true);
            isUnlocked = true;
            Debug.Log($"Achievement {idName} unlocked");
        }
        
        public List<CollectionAch> collection;
    }
}