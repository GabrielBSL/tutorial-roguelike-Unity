using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float lifeTime;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int poolSize;

    private InputActions m_InputActions;
    private Queue<GameObject> projectilePool;

    private float m_FireRateTimer;
    private bool m_ShootPerformed;

    void Awake()
    {
        m_InputActions = new InputActions();

        m_InputActions.Player.Shoot.performed += ctx => ShootPerformed(ctx);
        m_InputActions.Player.Shoot.canceled += ctx => ShootPerformed(ctx);

        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject newProjectile = Instantiate(shootPrefab);

            projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
    }

    private void OnEnable() => m_InputActions.Enable();
    private void OnDisable() => m_InputActions.Disable();

    void Update()
    {
        AjustRotation();

        m_FireRateTimer += Time.deltaTime;

        if(m_FireRateTimer > fireRate && m_ShootPerformed)
        {
            ShootProjectile();
            m_FireRateTimer = 0;
        }
    }

    private void AjustRotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private void ShootPerformed(InputAction.CallbackContext ctx)
    {
        m_ShootPerformed = ctx.performed;
    }

    private void ShootProjectile()
    {
        GameObject shoot = projectilePool.Dequeue();

        shoot.SetActive(true);
        shoot.transform.position = spawnPoint.position;
        shoot.transform.rotation = transform.rotation;
        shoot.GetComponent<Projectile>().DeactivateProjectile(lifeTime);

        projectilePool.Enqueue(shoot);
    }
}
