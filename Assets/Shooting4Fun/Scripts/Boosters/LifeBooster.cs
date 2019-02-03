using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBooster : Booster
{
    public float lifeToGive = 1000;

    protected override bool ApplyBoost(GameObject player)
    {
        var health = player.GetComponent<Health>();
        if (!health.IsFull()){
            health.ReceiveHealth(lifeToGive);
            return true;
        }

        return false;
    }
}
