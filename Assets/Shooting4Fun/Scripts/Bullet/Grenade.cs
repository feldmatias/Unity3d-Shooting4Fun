using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ExplodingBullet
{

    [Header ("Grenade")]
    public float timeToExplode = 5;

    private float explodeTimer = 0;

    protected override void Update()
    {
        explodeTimer += Time.deltaTime;
        if (explodeTimer >= timeToExplode)
        {
            Explode();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        // Do nothing
    }

}
