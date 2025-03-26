using System.Linq;
using Scripts.UnityActions;
using UnityEngine;

namespace Scripts.Achievements
{
   public class TestScript : MonoBehaviour
   {
      public AchievementData achData;

      private void OnEnable()
      {
         foreach (var achievement in achData.achievements.Where(achievement => achievement.action != null))
         {
            achievement.action.RaiseEvent += RaiseAchievement;
         }
      }
      private void OnDisable()
      {
         foreach (var achievement in achData.achievements.Where(achievement => achievement.action != null))
         {
            achievement.action.RaiseEvent -= RaiseAchievement;
         }
      }
      private void RaiseAchievement(GameAction _)
      {
         foreach (var achievement in achData.achievements.Where(achievement => achievement.action == _))
         {
            achievement.CheckProgress(_);
         }
      }
   }
}
