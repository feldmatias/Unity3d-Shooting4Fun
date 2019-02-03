using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunitionBooster : Booster
{
    protected override bool ApplyBoost(GameObject player)
    {
        var weapons = player.GetComponent<Player>().weapons;
        foreach (var weapon in weapons){
            weapon.AddMunition();
        }
        return true;
    }
}
