using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{

    public float radius = 3;
    public float checkInterval = 0.05f;

    private Vector3 originPosition;
    private float checkTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval){
            checkTimer = 0;
            DetectEnemies();
        }
    }

    private void DetectEnemies()
    {
        var hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hits){
            if (hit.gameObject.tag == Tags.ENEMY){
                hit.gameObject.GetComponent<Enemy>().BulletDetected(originPosition);
            }
        }
    }
}
