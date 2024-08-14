using System;
using System.Collections.Generic;
using Scripts.Data;
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
    [SerializeField] private IntData bulletCount;
    [SerializeField] public List<BulletBehavior> bulletPool;
    public bool canShoot;
    private float timer;

    private void Awake()
    {
        bulletPool = new List<BulletBehavior>();
        for (int i = 0; i < bulletCount.value; i++)
        {
            BulletBehavior bullet = Instantiate(bB);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (canShoot)
        {
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
        
    }

    private void Fire()
    {
        bB.ResetBullet();
        Vector3 direction = fireTransform.forward;
        var eulerAngles = fireTransform.eulerAngles;
        Quaternion newRotation = Quaternion.Euler(0, eulerAngles.y + 180, 0);
        
        BulletBehavior bullet = GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = fireTransform.position;
            bullet.transform.rotation = newRotation;
            bullet.gameObject.SetActive(true);
            bullet.Shoot(direction.normalized);
            bullet.bounce = 0;
        }
    }
    
    public void ToggleShootOn()
    {
        canShoot = true;
    }
    public void ToggleShootOff()
    {
        canShoot = false;
    }
    
    private BulletBehavior GetBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
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
