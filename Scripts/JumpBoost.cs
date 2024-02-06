using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float jumpForce;
    private float gravityValue = -9.81f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        
        if (playerController != null)
        {
            playerController.playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue);
        }
        playerController.playerVelocity.y += gravityValue * Time.deltaTime;
    }
}


