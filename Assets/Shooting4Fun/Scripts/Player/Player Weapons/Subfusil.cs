using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subfusil : PlayerWeapon
{

    protected override void PlayShootAudio()
    {
        AudioManager.Instance.PlaySubfusilShoot(transform.position);
    }

}
