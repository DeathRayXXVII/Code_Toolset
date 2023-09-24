using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool unbreakable;
    public Material[] materials;
    private Renderer rend;
    private int materialIndex;
    public float maxBounceAngle = 75.0f;

    private void Start()
    {
        health = maxHealth;
        rend = GetComponent<Renderer>();
        Hit();
        
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
        if (materialIndex >= 0 && materialIndex < materials.Length)
        {
            rend.material = materials[materialIndex];
        }
                
    }
}
