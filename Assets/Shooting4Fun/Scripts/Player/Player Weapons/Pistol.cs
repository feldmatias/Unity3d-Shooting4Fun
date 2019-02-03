using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PlayerWeapon
{

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlayPistolShoot(transform.position);
    }

}
