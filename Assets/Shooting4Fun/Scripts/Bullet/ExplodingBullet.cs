using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : Bullet
{
    [Header ("Explosion")]
    public GameObject explosion;

    protected bool exploded = false;

    protected void Explode()
    {
        if (exploded){
            return;
        }

        exploded = true;

        foreach (Transform child in transform){
            child.gameObject.SetActive(false);
        }

        explosion.SetActive(true);
        Invoke("Delete", 10);
    }
}
