using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
   public static GameState Instance { get; private set; }
   public string CurrentAction { get; set; }
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
