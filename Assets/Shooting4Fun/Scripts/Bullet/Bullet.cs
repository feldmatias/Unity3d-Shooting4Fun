using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500;
    public float timeToLive = 5;
    public string targetTag;
    public float damage;

    private float timer;
    protected Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = speed * transform.forward;
        timer = timeToLive;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Delete();
        }
    }

    protected void Delete()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.BULLET){
            return;
        }

        if (other.gameObject.tag == targetTag){
            other.gameObject.GetComponent<Health>().ReceiveDamage(damage);
        }

        Delete();
    }

    public void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
}
