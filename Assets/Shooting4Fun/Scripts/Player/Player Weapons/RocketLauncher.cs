using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : PlayerWeapon
{

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlayRocketLauncherShoot(transform.position);
    }

}
