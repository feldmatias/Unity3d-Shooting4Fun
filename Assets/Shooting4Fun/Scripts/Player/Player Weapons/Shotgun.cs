using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerWeapon
{

    [Header ("Shotgun")]
    public float shootRadius = 1f;

    protected override void ShootBullets()
    {
        foreach (var direction in GetBulletsDirections())
        {
            var bullet = GetBullet();
            bullet.transform.forward = direction;
            CurrentAmmo--;
        }
    }

    private List<Vector3> GetBulletsDirections()
    {
        var directions = new List<Vector3>();

        directions.Add(transform.forward); //Center
        directions.Add((transform.forward + transform.right * shootRadius).normalized); //Right
        directions.Add((transform.forward - transform.right * shootRadius).normalized); //Left
        directions.Add((transform.forward + transform.up * shootRadius).normalized); //Up
        directions.Add((transform.forward - transform.up * shootRadius).normalized); //Down

        return directions;
    }

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlayShotgunShoot(transform.position);
    }

}
