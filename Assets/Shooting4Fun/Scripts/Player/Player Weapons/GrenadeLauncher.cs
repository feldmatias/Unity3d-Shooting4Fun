using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : PlayerWeapon
{
    protected override void ShootBullets()
    {
        base.ShootBullets();
        Reload(); //Automatically reloads
    }

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlayGrenadeLauncherShoot(transform.position);
    }
}
