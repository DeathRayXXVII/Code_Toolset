using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference fireControl;
    public BulletData bulletData;
    public Transform fireTransform;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= bulletData.timeBetweenShots)
        {
            if (fireControl.action.triggered)
            {
                Fire();
                timer = 0f;
            }
        }
    }

    private void Fire()
    {
        // Adjust the fireTransform's rotation by adding a 90-degree offset
        Quaternion correctedRotation = fireTransform.rotation * Quaternion.Euler(0, -90, 0);

        // Instantiate the bullet with the corrected rotation
        Instantiate(bulletData.shellPrefab, fireTransform.position, correctedRotation);
        
    }
    
    private void OnEnable()
    {
        fireControl.action.Enable();
    }
    
    private void OnDisable()
    {
        fireControl.action.Disable();
    }
}
