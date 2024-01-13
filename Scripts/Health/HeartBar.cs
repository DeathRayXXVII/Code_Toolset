using System.Collections.Generic;
using Scripts.Data;
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
    List<HealthHeart> hearts = new List<HealthHeart>();
    public UnityEvent updateHeartBar;

    public void Start()
    {
        DrawHearts();
        updateAction.raiseNoArgs += UpdateHearts;
    }
    
    public void UpdateHearts()
    {
        DrawHearts();
        updateHeartBar.Invoke();
    }

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemander = maxHealth.value % 4;
        int makeHearts = (int)(maxHealth.value / 4 + maxHealthRemander);
        for (int i = 0; i < makeHearts; i++)
        {
            CreateHeart();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemander = (int)Mathf.Clamp(curtHealth.value - (i * 4), 0, 4);
            hearts[i].SetHeartState((HeartState)heartStatusRemander);
        }
    }
    
    public void CreateHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform, true);
        
        HealthHeart heart = newHeart.GetComponent<HealthHeart>();
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
