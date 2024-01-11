using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BrickData")]
public class BrickData : ScriptableObject
{
    public List<Material> material;
    public int health;
    public int maxHealth;
    public bool unbreakable = false;
    public Material unbreakableMaterial;
    public int score;
    public float spawnWeight = 1f;

    public void SetHealth()
    {
        health = material.Count;
    }

    public void Unbreakable()
    {
        unbreakable = true;
        
    }
}
