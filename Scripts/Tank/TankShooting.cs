using UnityEngine;
using UnityEngine.InputSystem;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference fireControl;
    [SerializeField] private InputActionReference bombControl;
    [SerializeField] private BulletBehavior bB;
    [SerializeField] private BulletData bulletData;
    [SerializeField] private BulletData bombData;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Transform bombTransform;
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
        if (timer >= bombData.timeBetweenShots)
        {
            if (bombControl.action.triggered)
            {
                Bomb();
                timer = 0f;
            }
        }
    }

    private void Fire()
    {
        Vector3 direction = fireTransform.up;
        BulletBehavior bullet = Instantiate(bB, fireTransform.position, fireTransform.rotation);
        bullet.Shoot(direction.normalized);
    }

    private void Bomb()
    {
        Instantiate(bombData.shellPrefab, bombTransform.position, bombTransform.rotation);
    }
    
    private void OnEnable()
    {
        fireControl.action.Enable();
        bombControl.action.Enable();
    }
    
    private void OnDisable()
    {
        fireControl.action.Disable();
        bombControl.action.Disable();
    }
}
