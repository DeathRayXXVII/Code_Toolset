using System.Collections.Generic;
using Scripts.Health;
using Scripts.UnityActions;
using UnityEngine;
using UnityEngine.Events;

public class HeartBar : MonoBehaviour
{
    public GameAction updateAction;
    public GameObject heartPrefab;
    public FloatData curtHealth;
    public FloatData maxHealth;
    private readonly List<HealthHeart> hearts = new List<HealthHeart>();
    public UnityEvent updateHeartBar;

    public void Start()
    {
        DrawHearts();
        updateAction.RaiseEvent += UpdateHearts;
    }
    
    public void UpdateHearts(GameAction _)
    {
        DrawHearts();
        updateHeartBar.Invoke();
    }

    public void DrawHearts()
    {
        ClearHearts();
        var maxHealthRemainder = maxHealth.value % 4;
        var makeHearts = (int)(maxHealth.value / 4 + maxHealthRemainder);
        for (var i = 0; i < makeHearts; i++)
        {
            CreateHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(curtHealth.value - (i * 4), 0, 4);
            hearts[i].SetHeartState((HeartState)heartStatusRemainder);
        }
    }
    
    public void CreateHeart()
    {
        var newHeart = Instantiate(heartPrefab, transform, true);
        
        var heart = newHeart.GetComponent<HealthHeart>();
        heart.SetHeartState(HeartState.Empty);
        hearts.Add(heart);
    }
    
    public void ClearHearts()
    {
        foreach (var heart in hearts)
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();
    }
}
