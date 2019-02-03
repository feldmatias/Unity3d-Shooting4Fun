using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 5f;
    public float damage = 100f;
    private Collider parent; 

    // Start is called before the first frame update
    void OnEnable()
    {
        parent = GetComponentInParent<Collider>();
        AudioManager.Instance.PlayExplosionAudio(transform.position);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits){
            Health health = hit.gameObject.GetComponent<Health>();
            if (health != null){
                //Make sure is not protected by another object
                RaycastHit rayHit;
                var direction = (transform.position - hit.gameObject.transform.position).normalized;
                if (Physics.Raycast(hit.gameObject.transform.position, direction, out rayHit, radius)){
                    if (rayHit.collider == parent){
                        health.ReceiveDamage(damage);
                    }
                }
            }
        }
    }

    
}
