using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool unbreakable;
    public Material[] materials;
    private Renderer rend;
    private int materialIndex;

    private void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
        Hit();
        
    }
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Ball")
            {
                if (unbreakable) 
                    return;
                health--;
                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Hit();
                }
            }
        }
    private void Hit()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        materialIndex = maxHealth - health;
        if (materialIndex >= 0 && materialIndex < materials.Length)
        {
            rend.material = materials[materialIndex];
        }
                
    }
}
