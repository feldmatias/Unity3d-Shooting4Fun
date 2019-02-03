using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    [Header("Info")]
    public string weaponName;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float precisionOffset;
    public bool shootsContinually = false;
    public float shootTimeout = 0.1f;

    [Header("Munitions")]
    public int munition;
    public int munitionToAdd;
    public int clipSize;
    public int CurrentAmmo {get;  protected set;}
    public float damage = 20f;

    private Quaternion originalRotation;
    private Player player;
    private GameObject bulletHolder;

    private float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        player = GetComponentInParent<Player>();
        bulletHolder = AssetsManager.Instance.BulletHolder;
        CurrentAmmo = clipSize;
        shootTimer = shootTimeout;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        shootTimer += Time.deltaTime;
    }

    private void Rotate()
    {
        if (player.playerAim.IsAiming) {
            var targetRotation = Quaternion.LookRotation(Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(Camera.main.transform.position, 0.08f, Camera.main.transform.forward, out hit)){
                targetRotation = Quaternion.LookRotation((hit.point - transform.position).normalized);
            }

            transform.rotation = targetRotation;

        } else {
            transform.localRotation = originalRotation;
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Shoot()
    {
        if (shootTimer >= shootTimeout)
        {
            if (CurrentAmmo > 0){
                shootTimer = 0;
                ShootBullets();
                PlayShootAudio();
            } else
            {
                AudioManager.Instance.PlayNoAmmo(transform.position);
            }
        }
    }

    protected virtual void PlayShootAudio() { }

    protected virtual void ShootBullets()
    {
        GetBullet();
        CurrentAmmo--;
    }

    protected GameObject GetBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletHolder.transform);
        bullet.transform.position = transform.position + transform.forward;

        var horizontalOffset = Random.Range(-precisionOffset, precisionOffset);
        var verticalOffset = Random.Range(-precisionOffset, precisionOffset);
        bullet.transform.forward = (transform.forward + transform.right * horizontalOffset + transform.up * verticalOffset).normalized;

        bullet.GetComponent<Bullet>().SetDamage(damage);
        return bullet;
    }

    public void Reload()
    {
        var missingAmmo = clipSize - CurrentAmmo;
        var ammoReloaded = Mathf.Min(missingAmmo, munition);

        CurrentAmmo += ammoReloaded;
        munition -= ammoReloaded;

        shootTimer = 0;

        AudioManager.Instance.PlayWeaponReload(transform.position);
    }

    public void AddMunition()
    {
        munition += munitionToAdd;
    }
}
