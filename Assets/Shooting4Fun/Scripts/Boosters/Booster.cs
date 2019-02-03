using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Booster : MonoBehaviour
{

    public float rechargeDuration = 5 * 60;


    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER){
            if (ApplyBoost(other.gameObject)){
                AudioManager.Instance.PlayBosoterPickAudio(transform.position);
                Disable();
            }
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        Invoke("Enable", rechargeDuration);
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }

    protected abstract bool ApplyBoost(GameObject player);
}
