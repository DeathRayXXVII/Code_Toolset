using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool unbreakable;
    public List<Material> materials;
    private Renderer rend;
    //public MaterialList materialsList;
    private int materialIndex;
    public float maxBounceAngle = 75.0f;
    private BrickData brickData;

    private void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
        Hit();
        
    }
    
    public void ResetBrick()
        {
            if (health >= 0 && !unbreakable)
            {
                gameObject.SetActive(true);
                health = materials.Count;
                rend.material = materials[rend.materials.Length - 1];
            }
        }
    public void SetHealth(int value)
    {
        health = value;
    }

    private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                Vector3 paddlePosition = transform.position;
                Vector2 ballPosition = collision.transform.position;

                float offset = paddlePosition.x - ballPosition.x;
                float width = collision.collider.bounds.size.x / 2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
                float bounceAngle = (offset / width) * maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
            
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rb.velocity = rotation * Vector2.up * ball.rb.velocity.magnitude;
            }
            if (ball)
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
        if (materialIndex >= 0 && materialIndex < materials.Count)
        {
            rend.material = materials[materialIndex];
        }
                
    }
}
