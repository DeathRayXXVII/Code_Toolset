using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool unbreakable;
    public List<Material> materials;
    public Renderer rend;
    private int materialIndex;
    public float maxBounceAngle = 75.0f;
    private BrickData brickData;
    public GameManager gameManager;
    public BrickDataList brickDataList;
    public UnityEvent hitEvent;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health = maxHealth;
        rend = GetComponent<Renderer>();
        Hit();
        
    }
    
    public void ResetBrick()
        {
            
            gameObject.SetActive(true);
            
            materials.Clear();
            // Get a random BrickData object from the list
            int randomIndex = Random.Range(0, brickDataList.brickData.Count);
            BrickData randomBrickData = brickDataList.brickData[randomIndex];
            
            // Add the BrickData object's materials to the Brick object's material list
            //Brick brickComponent = spawnedObject.GetComponent<Brick>();
            if (gameObject != null)
            {
                materials.AddRange(randomBrickData.material);
                health = randomBrickData.health;
                maxHealth = randomBrickData.maxHealth;
                unbreakable = randomBrickData.unbreakable;
            }
            else
            {
                Debug.LogError("The spawned object does not have a Brick component for materials.");
            }
            
            // Set the object's material based on the BrickData object
            //Renderer rend = spawnedObject.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = randomBrickData.material[0];
            }
            else
            {
                Debug.LogError("The spawned object does not have a Renderer component for material.");
            }
            /*if (health >= 0 && !unbreakable)
            {
                gameObject.SetActive(true);
                health = maxHealth;
                rend.material = materials[0];
            }*/
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
                ball.rb.velocity = Vector3.Reflect(ball.rb.velocity, collision.contacts[0].normal);
               /* Vector3 paddlePosition = transform.position;
                Vector2 ballPosition = collision.transform.position;

                float offset = paddlePosition.x - ballPosition.x;
                float width = collision.collider.bounds.size.x / 2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rb.velocity);
                float bounceAngle = (offset / width) * maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
            
                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
                ball.rb.velocity = rotation * Vector2.up / ball.rb.velocity.magnitude;*/
            }
            if (ball)
            {
                if (unbreakable) 
                    return;
                hitEvent.Invoke();
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
    
    public void ManagerHit()
    {
        gameManager.Hit();
    }
}
