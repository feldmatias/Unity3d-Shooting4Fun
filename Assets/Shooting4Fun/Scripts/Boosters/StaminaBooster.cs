using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBooster : Booster
{
    public float staminaToGive = 50;

    protected override bool ApplyBoost(GameObject player)
    {
        var stamina = player.GetComponent<Stamina>();
        if (!stamina.IsFull()){
            stamina.AddStamina(staminaToGive);
            return true;
        }

        return false;
    }
}
