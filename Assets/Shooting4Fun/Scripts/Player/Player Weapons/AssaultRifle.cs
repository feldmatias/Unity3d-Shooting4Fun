using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : PlayerWeapon
{

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlayAssaultRifleShoot(transform.position);
    }

}
