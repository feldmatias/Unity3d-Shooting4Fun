using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyWeapon : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float precisionOffset;
    public float shootTimeout = 0.2f;

    private Transform target;

    public bool IsAttacking { get; set; }

    private GameObject bulletHolder;
    private float shootTimer;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        target = AssetsManager.Instance.Player.transform;
        bulletHolder = AssetsManager.Instance.BulletHolder;
        shootTimer = shootTimeout;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0){
            shootTimer = shootTimeout;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (IsAttacking){
            var bullet = Instantiate(bulletPrefab, bulletHolder.transform);
            bullet.transform.position = transform.position + transform.forward;

            var horizontalOffset = Random.Range(-precisionOffset, precisionOffset);
            var verticalOffset = Random.Range(-precisionOffset, precisionOffset);
            bullet.transform.forward = (transform.forward + transform.right * horizontalOffset + transform.up * verticalOffset).normalized;

            AudioManager.Instance.PlayAssaultRifleShoot(transform.position);
        }
    }

    private void Aim()
    {
        if (IsAttacking){
            transform.forward = (target.position - transform.position).normalized;
        } else{
            transform.localRotation = originalRotation;
        }
    }
}
