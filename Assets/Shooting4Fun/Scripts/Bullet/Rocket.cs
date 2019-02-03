using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ExplodingBullet
{

    [Header("Rocket")]
    public float deviation = 0.01f;

    protected override void Update()
    {
        ApplyDeviation();
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.BULLET)
        {
            return;
        }

        Explode();
    }

    private void ApplyDeviation()
    {
        var verticalDeviation = Random.Range(-deviation, deviation);
        var horizontalDeviation = Random.Range(-deviation, deviation);

        rigidBody.AddForce(transform.right * horizontalDeviation);
        rigidBody.AddForce(transform.up * verticalDeviation);
    }
}
